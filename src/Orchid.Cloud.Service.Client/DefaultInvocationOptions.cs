using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Orchid.Cloud.Service.Client.Abstractions;

namespace Orchid.Cloud.Service.Client
{
    public class DefaultInvocationOptions
    {
        public FailureStrategy FailureStrategy { get; private set; } = FailureStrategy.Failover;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryTimes { get; private set; } = 1;

        /// <summary>
        /// 超时时间（秒）
        /// </summary>
        public double Timeout { get; private set; } = 5;

        /// <summary>
        /// 在调用失败后的处理，配合FailureStrategy来处理失败后的结果
        /// </summary>
        public Func<object[], Exception, object> FailCallback { get; set; }

        public IEnumerable<IExecutingFilter> ExecutingFilters { get; set; }

        public IEnumerable<IExecutedFilter> ExecutedFilters { get; set; }
    }

    /// <summary>
    /// 失败处理策略
    /// </summary>
    public enum FailureStrategy
    {
        /// <summary>
        /// 失败自动切换，当出现失败，重试若干次(缺省)
        /// 通常用于读操作，但重试会带来更长延迟。
        /// 可通过RetryTimes = "2" 来设置重试次数(不含第一次)。
        /// </summary>
        Failover,
        /// <summary>
        /// 快速失败，只发起一次调用，失败立即报错。
        /// 通常用于非幂等性的写操作，比如新增记录。
        /// </summary>
        Failfast,
        /// <summary>
        /// 失败安全，出现异常时，直接忽略。通常用于写入审计日志等操作。
        /// </summary>
        Failsafe,
        /// <summary>
        /// 失败自动恢复，后台记录失败请求，定时重发。
        /// 通常用于消息通知操作。
        /// </summary>
        Failback,
        ///并行调用多个服务器，只要一个成功即返回。
        /// 通常用于实时性要求较高的读操作，但需要浪费更多服务资源。
        /// 可通过forks = "2"来设置最大并行数。
        //Forking
    }
}