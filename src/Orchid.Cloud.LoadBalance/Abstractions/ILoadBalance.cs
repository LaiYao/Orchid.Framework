using System;

namespace Orchid.Cloud.LoadBalance.Abstractions
{
    public interface ILoadBalance
    {
        /// <summary>
        /// 根据给定的target获取实际访问的服务器地址
        /// </summary>
        /// <param name="target">服务器列表源</param>
        /// <returns></returns>
        string SelectActualUri(string target);
    }
}
