using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.ServiceRegistry.Consol
{
    public class ConsulServiceRegistryOptions
    {
        public string Host { get; set; }
        public int Port { get; set; } = 2181;
        /// <summary>
        /// 是否启用Consul的心跳检测
        /// </summary>
        public bool UseHealthCheck { get; set; }
        /// <summary>
        /// 访问Consul的Token
        /// </summary>
        public string Token { get; set; } = null;
        public string ConsistencyMode { get; set; } = "default";
        public double WatchIntervalSeconds { get; set; } = 5;
    }
}
