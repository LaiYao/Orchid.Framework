using Orchid.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMP.Domain.Entities
{
    public class Service : IHaveLables, IHasKey<string>
    {
        public string Id { get; set; }

        /// <summary>
        /// 服务契约名称，通常是接口全名
        /// </summary>
        public string ContractName { get; set; }

        public Dictionary<string, string> Lables { get; set; }

        /// <summary>
        /// 服务所属组，用于权限控制
        /// </summary>
        public string GroupName { get; set; }

        public Dictionary<string, string> Configurations { get; set; }

        public Dictionary<string, string> EnvironmentVariables { get; set; }

        public string ClusterID { get; set; }

        public int Port { get; set; }

        public string NamespaceName { get; set; }
    }
}
