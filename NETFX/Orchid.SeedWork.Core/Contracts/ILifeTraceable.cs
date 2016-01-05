using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.Core.Contracts
{
    public interface ILifeTraceable
    {
        bool IsNew { get; set; }
        bool IsDirty { get; set; }
        bool IsDelete { get; set; }
    }
}
