using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.DDD.Domain;

namespace Orchid.LocalizationWithinDB.Entities
{
    public class Resource : Entity<int>
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
