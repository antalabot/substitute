using Substitute.Business.DataStructs.Guild;
using Substitute.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IDiscordBotRestService
    {
        Task<IEnumerable<GuildModel>> GetGuilds();
        Task<IEnumerable<RoleModel>> GetGuildUserRoles(ulong guildId, ulong userId);
        Task<IEnumerable<RoleModel>> GetGuildRoles(ulong guildId);
        Task<EAccessLevel> GetGuildUserAccessLevel(ulong guildId, ulong userId);
    }
}
