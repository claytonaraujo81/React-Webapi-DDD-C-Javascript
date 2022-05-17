using System;
using System.ComponentModel.DataAnnotations;

namespace api.Domain.Entitiers
{

    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateAt { get; set; }
    }

}

