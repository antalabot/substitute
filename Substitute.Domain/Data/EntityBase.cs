using System;
using System.Collections.Generic;
using System.Text;

namespace Substitute.Domain.Data
{
    public abstract class EntityBase : IEntity
    {
        public ulong Id { get; set; }
    }
}
