using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public class CacheEntity : CacheEntityOptions, ICacheEntity
    {
        public string Key { get; internal set; }
        public object Value { get; set; }
    }
}
