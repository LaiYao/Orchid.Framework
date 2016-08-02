using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Caching.Abstractions
{
    public interface ICache
    {
        CacheLevel CacheLevel { get; }

        /// <summary>
        /// 设置缓存值到指定Region
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheTime">缓存过期时间，单位分钟</param>
        void Set<T>(string key, string region, T value,int cacheTime);

        /// <summary>
        /// 获取指定区域的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <returns>缓存值</returns>
        T Get<T>(string key, string region);

        /// <summary>
        /// 移除指定区域的缓存条目
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        void Remove(string key, string region);

        /// <summary>
        /// 是否存在缓存键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool Contains(string key, string region);
    }
}
