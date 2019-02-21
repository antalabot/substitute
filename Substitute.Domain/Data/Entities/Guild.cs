using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Substitute.Domain.Data.Entities
{
    public class Guild : EntityBase, IEntity
    {
        [Required]
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public ulong OwnerId { get; set; }
        public User Owner { get; set; }

        public ICollection<GuildRole> GuildRoles { get; set; }
        public ICollection<ImageResponse> ImageResponses { get; set; }
    }
}
