using App.Component.Common.Contracts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.Component.ShorteningUrlTest.CacheTest
{
    public class MemoryCacheTest
    {
        private IShortURLCache cacheService;        
        private string FirstCreatedKey;
        private string createdUrl;
        public MemoryCacheTest()
        {
            cacheService = ServiceProviderFactory.GetServiceProvider().GetShortURLCacheService();
            CreateNewURLInMainStorage();
        }
        
        private void CreateNewURLInMainStorage()
        {
            var shortURLStorageService= ServiceProviderFactory.GetServiceProvider().GetShortURLStorageService();
            FirstCreatedKey = UrlGenerator.RandomString(6);
            createdUrl = UrlGenerator.GenerateUniqueURL(FirstCreatedKey);
            FirstCreatedKey = shortURLStorageService.CreateNewURLAsync(FirstCreatedKey, createdUrl).Result;            
        }
        [Test, Order(1)]
        public void KeyExsitsInDbNotInCache()
        {
            string url;
            bool? result;
            result=cacheService.TryGetURL(FirstCreatedKey, out url);
            Assert.IsTrue(result);
            Assert.AreEqual(createdUrl, url);
        }

        [Test, Order(2)]
        public void KeyExsitsInCache()
        {
            string url;
            bool? result;
            result = cacheService.TryGetURL(FirstCreatedKey, out url);
            Assert.IsTrue(result);
            Assert.AreEqual(createdUrl, url);
        }
        [Test, Order(3)]
        public void KeyNotInCacheAndNotInDb()
        {
            string url;
            string key = UrlGenerator.RandomString(6);
            bool? result;
            result = cacheService.TryGetURL(key, out url);
            Assert.IsFalse(result);
            Assert.IsNull(url);
        }
        
        public string CacheNewUrl(string url)
        {            
            string key = UrlGenerator.RandomString(6);            
            
            return cacheService.CacheNewUrl(key,url);                      
           
        }
        [Test, Order(4)]
        public void CacheRandomNewUrl()
        {
            string key = UrlGenerator.RandomString(6);
            string url = UrlGenerator.GenerateUniqueURL(key);
            string result;
            result = cacheService.CacheNewUrl(key, url);
            Assert.AreEqual(result, url);
            string url2;
            Assert.IsTrue(cacheService.TryGetURL(key, out url2));
            Assert.AreEqual(url, url2);
        }

    }

}
