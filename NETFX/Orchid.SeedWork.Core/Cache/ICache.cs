using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Cache
{
    public interface ICache
    {
        void SET(string key, object value);
        object GET(string key);
    }
}
