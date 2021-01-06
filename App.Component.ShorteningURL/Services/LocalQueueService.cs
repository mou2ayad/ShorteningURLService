using Microsoft.Extensions.Options;
using App.Component.Common.Models;
using App.Component.ShorteningURL.Contracts;
using App.Component.Utilities.APIClient;
using System;
using System.Collections.Generic;


namespace App.Component.ShorteningURL.Services
{
    public class LocalQueueService : ILocalQueueService
    {
        private readonly IRestAPICaller _restAPICaller;
        private readonly ShorteningUrlConfigs _config;
        private Queue<string> keys;
        private Queue<string> returnedKeys;
        private readonly object locker = new object();
        public LocalQueueService(IRestAPICaller restAPICaller, IOptions<ShorteningUrlConfigs> options)
        {
            _restAPICaller = restAPICaller;
            _config = options.Value;
            keys = new Queue<string>();
            returnedKeys = new Queue<string>();
            LoadKeysFromKGS();
        }
        public void LoadKeysFromKGS()
        {
            lock (locker)
            {
                if (keys.Count == 0)
                {
                    var results = _restAPICaller.CallAPIAsync<List<string>>(_config.KGSUrl, "KGS service").Result;
                    keys = new Queue<string>(results);
                }
            }
        }
        public string GetKey()
        {
            string newkey;
            if (!returnedKeys.TryDequeue(out newkey))
                if (!keys.TryDequeue(out newkey))
                    LoadKeysFromKGS();
            if (string.IsNullOrEmpty(newkey))
                throw new Exception("No more keys available");
            return newkey;
        }
        public void ReturnKey(string key)
        {
            returnedKeys.Enqueue(key);
        }

    }
}
