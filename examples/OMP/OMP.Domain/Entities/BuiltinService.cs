using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Core.Abstractions;

namespace OMP.Domain.Entities
{
    public class BuiltinService : INamable
    {
        public string Name { get; set; }

        public string ProviderName { get; set; }
    }
}
