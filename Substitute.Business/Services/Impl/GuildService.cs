using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services.Impl
{
    public class GuildService : IGuildService
    {
        public Task<RoleDigestModel> GetRoles(RoleFilterModel model)
        {
            throw new NotImplementedException();
        }

        public bool HasBotJoined(ulong guildId)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleAccessLevel(ulong guildId, ulong roleId, EAccessLevel accessLevel)
        {
            throw new NotImplementedException();
        }
    }
}
