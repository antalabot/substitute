using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Enums;
using System.Collections.Generic;

namespace Substitute.Business.Services
{
    public interface IRoleService
    {
        IEnumerable<object> List(RoleFilterModel filter);
        object SetAccessLevel(ulong id, EAccessLevel accessLevel, ulong guildId, ulong userId);
    }
}
