using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Abstractions;

namespace OMP.Website.Models
{
    public class BaseViewModel : INamable
    {
        public string Name { get; set; }

        public Dictionary<string, string> Breadcrumbs { get; set; }
    }

    public class BaseViewModel<T> : BaseViewModel
    {
        public T Model { get; set; }
    }
}
