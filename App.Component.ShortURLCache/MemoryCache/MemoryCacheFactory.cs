using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace App.Component.ShortURLCache
{
    public class MemoryCacheFactory<T, Y> 
    {
        private readonly IMemoryCache _cache;
        private readonly Func<T, Task<Y>> lazyAdd;
        private readonly int _expiresAfterInHours;    

        public MemoryCacheFactory(IMemoryCache cache,Func<T, Task<Y>> LazyAdd, int ExpiresAfterInHours)
        {
            _expiresAfterInHours = ExpiresAfterInHours;
            lazyAdd = LazyAdd;
            _cache = cache;
        }

        public virtual bool TryGetValue(T key, out Y result)
        {
            if (_cache.TryGetValue(key, out result))
                return true;
            var value = lazyAdd.Invoke(key).Result;
            if (value == null)
                return false;
            _cache.Set(key, value, GenerateDateTimeOffset());
            result = value;
            return true;
        }
        public Y this[T key]
        {
            get
            {
                Y temp;
                if (_cache.TryGetValue(key, out temp))
                    return temp;
                throw new Exception("key is not found in the Cache");
            }
        }
        public Y AddToCache(T key, Y value)
        {           
            return _cache.Set(key, value, GenerateDateTimeOffset());                      
        }


        public bool RemoveIfExists(T key)
        {
            Y tempValue;
            if (_cache.TryGetValue(key, out tempValue))
            {
                _cache.Remove(key);
                return true;
            }
            return false;
        }
        private DateTimeOffset GenerateDateTimeOffset() => new DateTimeOffset(DateTime.Now.AddHours(_expiresAfterInHours));
    }
}
