using Substitute.Domain.Enums;

namespace Substitute.Business.DataStructs.User
{
    public class UserGuildModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public EAccessLevel AccessLevel { get; set; }
    }
}
