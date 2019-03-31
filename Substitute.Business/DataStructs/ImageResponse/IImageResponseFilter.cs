using Substitute.Business.DataStructs.Enum;

namespace Substitute.Business.DataStructs.ImageResponse
{
    public interface IImageResponseFilter
    {
        string Command { get; }
        ulong? GuildId { get; }
        ulong UserId { get; }
        EImageResponseSort SortBy { get; }
    }
}
