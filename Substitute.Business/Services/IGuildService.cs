using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Substitute.Business.DataStructs.Guild;
using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services
{
    public interface IGuildService
    {
        Task<GuildModel> GetGuildData(ulong guildId);
        Task<IEnumerable<RoleDigestModel>> GetRoles(RoleFilterModel model);
        Task SetRoleAccessLevel(RoleModel role);
    }
}
