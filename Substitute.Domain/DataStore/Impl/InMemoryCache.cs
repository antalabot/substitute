using Microsoft.Extensions.Caching.Memory;
using Substitute.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore.Impl
{
    public class InMemoryCache : ICache
    {
        #region Private readonly variables
        private readonly IMemoryCache _cache;
        #endregion


        #region Constructor
        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }
        #endregion

        #region Implementation of ICache
        public TItem Get<TItem>(string key)
        {
            return _cache.Get<TItem>(key);
        }

        public TItem GetOrCreate<TItem>(string key, Func<ICacheEntity, TItem> factory)
        {
            return _cache.GetOrCreate(key, entry =>
            {
                CacheEntity entity = new CacheEntity();
                TItem item = factory(entity);
                entry.AbsoluteExpiration = entity.AbsoluteExpiration;
                entry.AbsoluteExpirationRelativeToNow = entity.AbsoluteExpirationRelativeToNow;
                entry.SlidingExpiration = entity.SlidingExpiration;
                return item;
            });
        }

        public async Task<TItem> GetOrCreateAsync<TItem>(string key, Func<ICacheEntity, Task<TItem>> factory)
        {
            return await _cache.GetOrCreate(key, async entry =>
            {
                CacheEntity entity = new CacheEntity();
                TItem item = await factory(entity);
                entry.AbsoluteExpiration = entity.AbsoluteExpiration;
                entry.AbsoluteExpirationRelativeToNow = entity.AbsoluteExpirationRelativeToNow;
                entry.SlidingExpiration = entity.SlidingExpiration;
                return item;
            });
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public TItem Set<TItem>(string key, TItem value)
        {
            return _cache.Set(key, value);
        }

        public TItem Set<TItem>(string key, TItem value, DateTimeOffset absoluteExpiration)
        {
            return _cache.Set(key, value, absoluteExpiration);
        }

        public TItem Set<TItem>(string key, TItem value, TimeSpan absoluteExpirationRelativeToNow)
        {
            return _cache.Set(key, value, absoluteExpirationRelativeToNow);
        }

        public TItem Set<TItem>(string key, TItem value, ICacheEntityOptions options)
        {
            MemoryCacheEntryOptions opts = new MemoryCacheEntryOptions();
            opts.AbsoluteExpiration = options.AbsoluteExpiration;
            opts.AbsoluteExpirationRelativeToNow = options.AbsoluteExpirationRelativeToNow;
            opts.SlidingExpiration = options.SlidingExpiration;
            return _cache.Set(key, value, opts);
        }

        public bool TryGetValue<TItem>(string key, out TItem value)
        {
            return _cache.TryGetValue(key, out value);
        }
        #endregion
    }
}
