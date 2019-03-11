using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public interface ISnowflake
    {
        ulong GetSnowflake();
    }
}
