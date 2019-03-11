namespace Substitute.Business.DataStructs.ImageResponse
{
    public class ImageResponseModel : ImageResponseDigestModel
    {
        public ulong? GuildId { get; set; }
        public byte[] Image { get; set; }
    }
}
