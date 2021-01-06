using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using App.Component.Common.Contracts;
using App.Component.Common.Models;


namespace App.Component.ShortURLCache.Cache
{
    public class ShortURLMemoryCache : IShortURLCache
    {
        private readonly MemoryCacheFactory<string, string> _memoryCacheFactory;
        private readonly IShortURLStorage _shortURLStorage;
        private readonly ShorteningUrlConfigs configs;
        public ShortURLMemoryCache(IMemoryCache cache, IShortURLStorage shortURLStorage, IOptions<ShorteningUrlConfigs> options)
        {
            configs = options.Value;
            _memoryCacheFactory = new MemoryCacheFactory<string, string>(cache,(key) => { return _shortURLStorage.GetUrlByKeyAsync(key); }, configs.ValuesInCacheExpireAfter);
            _shortURLStorage = shortURLStorage;
        }

        public string CacheNewUrl(string key, string FullUrl) =>
            _memoryCacheFactory.AddToCache(key, FullUrl);

        public bool TryGetURL(string key, out string FullUrl) =>
            _memoryCacheFactory.TryGetValue(key, out FullUrl);
        
    }
}
