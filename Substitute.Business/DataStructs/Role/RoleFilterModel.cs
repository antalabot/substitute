using Substitute.Business.DataStructs.Enum;
using Substitute.Domain.Enums;

namespace Substitute.Business.DataStructs.Role
{
    public class RoleFilterModel : FilterBase
    {
        public ulong GuildId { get; set; }
        public string Name { get; set; }
        public EAccessLevel? AccessLevel { get; set; }
        public ulong UserId { get; set; }
        public ERoleSort SortBy { get; set; }
    }
}
