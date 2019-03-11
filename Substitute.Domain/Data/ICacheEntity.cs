using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public interface ICacheEntity : ICacheEntityOptions
    {
        string Key { get; }
        object Value { get; set; }
    }
}
