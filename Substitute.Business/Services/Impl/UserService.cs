using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.User;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services.Impl
{
    public class UserService : IUserService
    {
        public Task<EAccessLevel> GetGuildAccessLevel(ulong userId, ulong guildId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GuildModel>> GetGuilds(string token)
        {
            throw new NotImplementedException();
        }

        public Task<UserDataModel> GetUserData(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOwnerSet()
        {
            throw new NotImplementedException();
        }

        public Task SetOwner(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
