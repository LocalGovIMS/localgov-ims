using System;
using System.Runtime.Caching;

namespace DataAccess.Classes.Caching
{
    public static class MemoryCache
    {
        static readonly object cacheLock = new object();

        public static T GetCachedData<T>(string cacheKey, Func<T> function)
        {
            return GetCachedData(cacheKey, function, 5); // TODO: Pickup the lifespanInMinutes from web.config
        }

        public static T GetCachedData<T>(string cacheKey, Func<T> function, int lifespanInMinutes)
        {
            // Returns null if the value does not exist.
            // Prevents a race condition where the cache invalidates between the 
            // contains check and the retreival.
            var cachedValue = System.Runtime.Caching.MemoryCache.Default.Get(cacheKey, null);

            if (cachedValue != null)
            {
                return (T)cachedValue;
            }

            lock (cacheLock)
            {
                // Check to see if anyone wrote to the cache while we where waiting our 
                // turn to write the new value.
                cachedValue = System.Runtime.Caching.MemoryCache.Default.Get(cacheKey, null);

                if (cachedValue != null)
                {
                    return (T)cachedValue;
                }

                // The value still did not exist so we now write it in to the cache.
                var dataToCache = function();

                CacheItemPolicy cip = new CacheItemPolicy()
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(lifespanInMinutes))
                };

                System.Runtime.Caching.MemoryCache.Default.Set(cacheKey, dataToCache, cip);

                return dataToCache;
            }
        }
    }
}
