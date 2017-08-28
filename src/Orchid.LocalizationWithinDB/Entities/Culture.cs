using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.DDD.Domain;
using Orchid.Core.Abstractions;

namespace Orchid.LocalizationWithinDB.Entities
{
    public class Culture : Entity<int>, INamable
    {
        public string Name { get; set; }

        public virtual List<Resource> Resources { get; set; }
    }
}
