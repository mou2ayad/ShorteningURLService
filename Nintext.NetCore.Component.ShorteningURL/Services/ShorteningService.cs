using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nintex.NetCore.Component.Common.Contracts;
using Nintex.NetCore.Component.Common.Models;
using Nintex.NetCore.Component.ShorteningURL.Contracts;
using Nintex.NetCore.Component.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.NetCore.Component.ShorteningURL.Services
{
    public class ShorteningService : IShorteningService
    {
        private readonly IShortURLCache _shortURLCache;
        private readonly IShortURLStorage _shortURLStorage;        
        private readonly ShorteningUrlConfigs _config;
        private readonly IUrlFormatService _urlFormatService;
        private readonly ILocalQueueService _localQueueService;
        private readonly ILogger<ShorteningService> _logger;
        public ShorteningService(IShortURLCache shortURLCache, IShortURLStorage shortURLStorage, IUrlFormatService urlFormatService, ILocalQueueService localQueueService, IOptions<ShorteningUrlConfigs> options,ILogger<ShorteningService> logger)
        {
            _shortURLCache = shortURLCache;
            _shortURLStorage = shortURLStorage;            
            _urlFormatService = urlFormatService;
            _localQueueService = localQueueService;
            _logger = logger;
            _config = options.Value;

        }
        public string GetActualUrlByShortkey(string shortkey)
        {
            string url;
            _shortURLCache.TryGetURL(shortkey, out url);
            return url;             
        }
        private async Task<string> TryToInsert(string formatedurl, string key, int attemptNumber = 1)
        {            
            try
            {
               return await _shortURLStorage.CreateNewURLAsync(key, formatedurl);
            }
            catch (SqlException sqlex)
            {
                if (sqlex.Number == 2601 && sqlex.Message.Contains("IX_ShortKey"))
                {
                    _logger.LogErrorDetails(new Exception("Attempt to insert duplicate key"), new { key, formatedurl, attemptNumber });
                    key = _localQueueService.GetKey();
                    if (attemptNumber < 3)                  
                        return await TryToInsert(formatedurl, key, ++attemptNumber);
                }
                throw sqlex;
            }
        }
        public async Task<string> RegisterUrlAsync(string AbsoluteUrl)
        {
            string formatedurl=_urlFormatService.FormateURL(AbsoluteUrl);
            if (string.IsNullOrEmpty(formatedurl))
                return null;
            string key = _localQueueService.GetKey();
            string newkey;
            try
            {
                newkey=await TryToInsert(formatedurl, key);                
            }            
            catch (Exception ex)
            {
                _localQueueService.ReturnKey(key);
                throw ex;
            }
            if (string.IsNullOrEmpty(newkey))
                throw new Exception("key is null");

            if (newkey != key)                
                _localQueueService.ReturnKey(key);

            _shortURLCache.CacheNewUrl(newkey, formatedurl);
            return $"{_config.BaseUrl}/{newkey}";
        }

    }
}
