using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.Services
{
    public interface IBotService
    {
        string GetJoinLink(ulong guildId, string returlUrl);
    }
}
