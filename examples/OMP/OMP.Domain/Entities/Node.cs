using Orchid.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OMP.Domain.Entities
{
    public class Node : INamable
    {
        public string Name { get; set; }

        public string Ip { get; set; }
    }
}