using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Agent
{
    public static class AgentConstants
    {
        /// <summary>
        /// 环境参数名称-代理端口
        /// </summary>
        public const string ENV_AGENT_PORT_KEY = "AGENT_PORT";
        /// <summary>
        /// 环境参数名称-内部实际服务端口
        /// </summary>
        public const string ENV_SERVICE_PORT_KEY = "SERVICE_PORT";

        #region | Service Registry |

        // Provider
        public const string SVCREG_PROVIDER_NAME_CONSUL = "consul";
        public const string SVCREG_PROVIDER_NAME_KUBE = "kube";
        public const string SVCREG_PROVIDER_NAME_ETCD = "etcd";

        #endregion
    }
}
