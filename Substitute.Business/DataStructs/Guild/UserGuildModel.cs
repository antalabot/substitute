using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Business.DataStructs.Guild
{
    public class UserGuildModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public bool IsOwner { get; set; }
        public bool CanManage { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
