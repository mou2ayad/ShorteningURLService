using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Persistence;
using Nintex.NetCore.Component.Common.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Service
{
    public class ShorteningUrlSqlServerStorageService : IShorteningUrlSqlServerStorageService , IShortURLStorage
    {
        private readonly IDesignTimeDbContextFactory<UrlShorteningDbContext> _dbContextFactory;
        public ShorteningUrlSqlServerStorageService(IDesignTimeDbContextFactory<UrlShorteningDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        private UrlShorteningDbContext GetContext() => _dbContextFactory.CreateDbContext(null);

        public async Task<Url> InsertURLRowInDBAsync(string ShortKey, string FullUrl)
        {
            List<Url> res;
            using (var context = GetContext())
            {
                var ShortKeyParam = new SqlParameter("ShortKey", ShortKey);
                var FullUrlParam = new SqlParameter("FullUrl", FullUrl);
                res = await context.Urls.FromSqlRaw("execute dbo.CreateNewUrl @ShortKey,@FullUrl", ShortKeyParam, FullUrlParam).ToListAsync();
            }
            if (res != null && res.Count > 0)
            {
                return res.FirstOrDefault();
            }
            return null;
        }

        public async Task<Url> SelectUrlRowByKeyAsync(string key)
        {
            List<Url> res;
            using (var context = GetContext())
            {
                var ShortKeyParam = new SqlParameter("ShortKey", key);
                res = await context.Urls.FromSqlInterpolated($"execute dbo.GetUrlByKey {key}").ToListAsync();
            }
            if (res != null && res.Count > 0)
            {
                return res.FirstOrDefault();
            }
            return null;
        }

        async Task<string> IShortURLStorage.GetUrlByKeyAsync(string key)
        {
            var result = await SelectUrlRowByKeyAsync(key);
            return result == null ? null : result.FullUrl;            
        }

        async Task<string> IShortURLStorage.CreateNewURLAsync(string ShortKey, string FullUrl)
        {
            var result = await InsertURLRowInDBAsync(ShortKey, FullUrl);
            return result == null ? null : result.ShortKey;
        }

       
    }
}
