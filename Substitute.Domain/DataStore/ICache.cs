using Substitute.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Substitute.Domain.DataStore
{
    public interface ICache
    {
        TItem Get<TItem>(string key);
        TItem GetOrCreate<TItem>(string key, Func<ICacheEntity, TItem> factory);
        Task<TItem> GetOrCreateAsync<TItem>(string key, Func<ICacheEntity, Task<TItem>> factory);
        TItem Set<TItem>(string key, TItem value);
        TItem Set<TItem>(string key, TItem value, DateTimeOffset absoluteExpiration);
        TItem Set<TItem>(string key, TItem value, TimeSpan absoluteExpirationRelativeToNow);
        TItem Set<TItem>(string key, TItem value, ICacheEntityOptions options);
        bool TryGetValue<TItem>(string key, out TItem value);
        void Remove(string key);
    }
}
