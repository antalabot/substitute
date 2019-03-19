using Substitute.Business.DataStructs.Enum;
using Substitute.Domain.Enums;

namespace Substitute.Business.DataStructs.Role
{
    public interface IRoleFilter
    {
        ulong GuildId { get; }
        string Name { get; }
        EAccessLevel? AccessLevel { get; }
        ulong UserId { get; }
        ERoleSort SortBy { get; }
    }
}
