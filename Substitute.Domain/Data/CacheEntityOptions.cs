using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public class CacheEntityOptions : ICacheEntityOptions
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
