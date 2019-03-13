using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services
{
    public interface IGuildService
    {
        bool HasBotJoined(ulong guildId);
        Task<RoleDigestModel> GetRoles(RoleFilterModel model);
        Task SetRoleAccessLevel(ulong guildId, ulong roleId, EAccessLevel accessLevel);
    }
}
