
using App.Component.Common.Contracts;


namespace App.Component.ShortURLCache.Cache
{
    public class ShortURLNoCache : IShortURLCache
    {        
        private readonly IShortURLStorage _shortURLStorage; 
        public ShortURLNoCache( IShortURLStorage shortURLStorage)
        {            
            _shortURLStorage = shortURLStorage;
        }

        public string CacheNewUrl(string key, string FullUrl)=> key;


        public bool TryGetURL(string key, out string FullUrl)
        {
            FullUrl= _shortURLStorage.GetUrlByKeyAsync(key).Result;
            return !string.IsNullOrEmpty(FullUrl);
                
        }
    }
}
