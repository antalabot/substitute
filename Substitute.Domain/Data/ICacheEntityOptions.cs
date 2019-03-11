using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public interface ICacheEntityOptions
    {
        DateTimeOffset? AbsoluteExpiration { get; set; }
        TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
        TimeSpan? SlidingExpiration { get; set; }
    }
}
