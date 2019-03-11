using Substitute.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.DataStructs.Guild
{
    public class Role
    {
        public Role()
        {
            AccessLevel = EAccessLevel.User;
        }

        public ulong Id { get; set; }
        public string Name { get; set; }
        public EAccessLevel AccessLevel { get; set; }
    }
}
