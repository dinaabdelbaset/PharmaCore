using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
