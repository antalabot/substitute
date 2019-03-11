using Substitute.Business.DataStructs.Guild;
using Substitute.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IDiscordBotRestService
    {
        Task<IEnumerable<Role>> GetGuildUserRoles(ulong guildId, ulong userId);
        Task<IEnumerable<Role>> GetGuildRoles(ulong guildId);
        Task<EAccessLevel> GetGuildUserAccessLevel(ulong guildId, ulong userId);
    }
}
