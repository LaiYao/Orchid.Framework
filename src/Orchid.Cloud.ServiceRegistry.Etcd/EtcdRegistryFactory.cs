using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.ServiceRegistry.Etcd
{
    public class EtcdRegistryFactory : IRegistryFactory
    {
        #region | Fields |

        IRegistry _registry;

        #endregion

        public IRegistry GetRegistry(RegistryOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
