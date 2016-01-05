using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Adapter
{
    public interface IAdapterFactory
    {
        IAdapter Create();
    }
}
