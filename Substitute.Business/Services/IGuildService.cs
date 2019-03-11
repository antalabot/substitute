using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.Services
{
    public interface IGuildService
    {
        bool HasBotJoined(ulong guildId);
    }
}
