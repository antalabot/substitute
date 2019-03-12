using System;
using System.Collections.Generic;
using System.Text;
using Substitute.Business.DataStructs.Role;
using Substitute.Domain.Enums;

namespace Substitute.Business.Services
{
    public class RoleService : IRoleService
    {
        public IEnumerable<object> List(RoleFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public object SetAccessLevel(ulong id, EAccessLevel accessLevel, ulong guildId, ulong userId)
        {
            throw new NotImplementedException();
        }
    }
}
