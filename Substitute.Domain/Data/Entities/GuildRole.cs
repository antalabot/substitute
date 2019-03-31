using Substitute.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Substitute.Domain.Data.Entities
{
    public class GuildRole : EntityBase, IEntity
    {
        public ulong GuildId { get; set; }
        [Required]
        public string Name { get; set; }
        public EAccessLevel AccessLevel { get; set; }
    }
}
