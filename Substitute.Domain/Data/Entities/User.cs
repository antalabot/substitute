using Substitute.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Substitute.Domain.Data.Entities
{
    public class User : EntityBase, IEntity
    {
        [Required]
        public string Name { get; set; }
        public ERole Role { get; set; }
        [Required]
        public string Discriminator { get; set; }
        [Required]
        public ushort DiscriminatorValue { get; set; }
        public string IconUrl { get; set; }

        public ICollection<Guild> OwnedGuilds { get; set; }
    }
}
