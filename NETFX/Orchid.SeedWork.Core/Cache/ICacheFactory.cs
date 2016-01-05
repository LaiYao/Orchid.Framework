using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Cache
{
    public interface ICacheFactory
    {
        ICache Create();
    }
}
