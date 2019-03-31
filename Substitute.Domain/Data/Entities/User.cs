using Substitute.Domain.Enums;

namespace Substitute.Domain.Data.Entities
{
    public class User : EntityBase, IEntity
    {
        public ERole Role { get; set; }
    }
}
