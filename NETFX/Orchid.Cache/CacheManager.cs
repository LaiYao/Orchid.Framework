using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Cache
{
    public static class CacheManager
    {
        #region | Properties |

        public static readonly List<ICache> CacheClients = new List<ICache>();
        
        #endregion
    }
}
