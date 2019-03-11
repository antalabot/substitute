using System.ComponentModel.DataAnnotations;

namespace Substitute.Domain.Data.Entities
{
    public class ImageResponse : EntityBase, IEntity
    {
        [Required]
        public string Command { get; set; }
        public ulong? GuildId { get; set; }
        public Guild Guild { get; set; }
        [Required]
        public string Fielename { get; set; }
    }
}
