using Orchid.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Abstractions
{
    public interface IBuiltinService : INamable
    {
        /// <summary>
        /// 内建服务角色
        /// </summary>
        BuiltinServiceRole Role { get; }

        string Description { get; set; }
    }

    public enum BuiltinServiceRole
    {
        /// <summary>
        /// 负载均衡
        /// </summary>
        Loadbalance,
        /// <summary>
        /// 日志
        /// </summary>
        Logging,
        /// <summary>
        /// 服务注册
        /// </summary>
        ServiceRegistry,
        /// <summary>
        /// 消息队列
        /// </summary>
        MessageQueue,
        /// <summary>
        /// 诊断矩阵
        /// </summary>
        Metrics,
        /// <summary>
        /// 协议
        /// </summary>
        Protocal
    }
}
