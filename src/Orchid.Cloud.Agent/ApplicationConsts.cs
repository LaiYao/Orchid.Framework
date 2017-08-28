using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Agent
{
    public static class ApplicationConsts
    {
        #region | Environments |

        public static readonly string ENV_SERVICE_NAME = "SERVICE_NAME";
        public static readonly string ENV_CONFIG_URLS = "ETCD_CONFIG_URLS";
        public static readonly string ENV_CONFIG_USER = "ETCD_CONFIG_USER";
        public static readonly string ENV_CONFIG_PWD = "ETCD_CONFIG_PWD";
        public static readonly string ENV_LOGGING_URLS = "KAFKA_LOGGING_URLS";

        #endregion

        #region | Configuration |

        public static readonly string CFG_SERVICE_ROOT_KEY = "services";

        #endregion

        #region | Logging |




        #endregion
    }

    public static class LoggingEvents
    {
        public static readonly int LOGGING_URLS = 0;
    }
}
