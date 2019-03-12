using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using Substitute.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Substitute.Business.Services
{
    public interface IUserService
    {
        Task<EAccessLevel> GetGuildAccessLevel(ulong userId, ulong guildId);
        Task<UserDataModel> GetUserData(string token);
        Task<IEnumerable<GuildModel>> GetGuilds(string token);
        Task<bool> IsOwnerSet();
        Task SetOwner(int userId);
    }
}
