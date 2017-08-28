using System;
using System.Collections.Generic;
using System.Text;
using Orchid.DDD.Domain;
using Orchid.DDD;
using Orchid.Core.Abstractions;

namespace OMP.Domain.Entities
{
    /// <summary>
    /// 通过Namespace进行资源隔离访问，跨Namespace访问需要AccessToken授权
    /// </summary>
    public class Namespace : INamable, IHaveLables
    {
        /// <summary>
        /// 默认均位于“deafult”命名空间下
        /// </summary>
        public string Name { get; set; } = "default";
        /// <summary>
        /// 通过Label标注其他属性
        /// </summary>
        public Dictionary<string, string> Lables { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 服务访问本命名空间的Token集合
        /// </summary>
        public Dictionary<Service, string> AccessTokens { get; set; }
    }
}
