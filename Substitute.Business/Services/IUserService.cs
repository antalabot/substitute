using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using Substitute.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IUserService
    {
        Task<EAccessLevel> GetGuildAccessLevel(ulong userId, string token, ulong guildId);
        Task<UserDataModel> GetUserData(ulong userId, string token);
        Task<IEnumerable<DataStructs.Guild.UserGuildModel>> GetGuilds(ulong userId, string token);
        Task<DataStructs.User.UserGuildModel> GetGuildData(ulong userId, string token, ulong guildId);
        Task<bool> IsOwnerSet();
        Task SetOwner(ulong userId, string token);
    }
}
