using System;
using System.Collections.Generic;
using System.Text;
using Orchid.DDD.Domain;

namespace OMP.Domain.Entities
{
    public abstract class BaseEntity : Entity<string>
    {
        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }
    }
}
