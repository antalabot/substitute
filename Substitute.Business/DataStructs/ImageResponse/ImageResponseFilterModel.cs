using Substitute.Business.DataStructs.Enum;

namespace Substitute.Business.DataStructs.ImageResponse
{
    public class ImageResponseFilterModel : FilterBase
    {
        public ImageResponseFilterModel()
            : base()
        {
            SortBy = EImageResponseSort.Command;
        }

        public string Command { get; set; }
        public ulong? GuildId { get; set; }
        public ulong UserId { get; set; }
        public EImageResponseSort SortBy { get; set; }
    }
}
