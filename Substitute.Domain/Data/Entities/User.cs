using Substitute.Domain.Enums;
using System.Collections.Generic;

namespace Substitute.Domain.Data.Entities
{
    public class User : EntityBase, IEntity
    {
        public ERole Role { get; set; }

        public ICollection<Guild> OwnedGuilds { get; set; }
    }
}
