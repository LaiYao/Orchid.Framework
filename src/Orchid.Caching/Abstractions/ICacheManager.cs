using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Caching.Abstractions
{
    public interface ICacheManager
    {
        /// <summary>
        /// 设置缓存值到指定Region
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheTime">缓存过期时间，单位分钟</param>
        void Set<T>(string key, T value, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = null);

        /// <summary>
        /// 设置缓存值到指定Region
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheTime">缓存过期时间，单位分钟</param>
        Task SetAsync<T>(string key, T value, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = null);

        /// <summary>
        /// 获取指定区域的缓存值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <param name="cacheTime">缓存过期时间，单位分钟</param>
        /// <returns>缓存值</returns>
        T Get<T>(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = null);

        /// <summary>
        /// 获取指定区域的缓存值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <param name="cacheTime">缓存过期时间，单位分钟</param>
        /// <returns>缓存值</returns>
        Task<T> GetAsync<T>(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = null);

        /// <summary>
        /// 移除指定区域的缓存条目
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        void Remove(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global);

        /// <summary>
        /// 移除指定区域的缓存条目
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        Task RemoveAsync(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global);

        void RemoveByPattern(string pattern, CacheLevel cacheLevel = CacheLevel.Global);

        /// <summary>
        /// 是否存在缓存键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <returns></returns>
        bool Contains(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global);

        /// <summary>
        /// 是否存在缓存键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        /// <returns></returns>
        Task<bool> ContainsAsync(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global);

        void ClearAll(CacheLevel cacheLevel = CacheLevel.Global);
        Task ClearAllAsync(CacheLevel cacheLevel = CacheLevel.Global);

        /// <summary>
        /// 通过区域名称清除缓存
        /// </summary>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        void Clear(string region = "", CacheLevel cacheLevel = CacheLevel.Global);
        /// <summary>
        /// 通过区域名称清除缓存
        /// </summary>
        /// <param name="region">区域名称</param>
        /// <param name="cacheLevel">缓存级别</param>
        Task ClearAsync(string region = "", CacheLevel cacheLevel = CacheLevel.Global);

        /// <summary>
        /// 是否线程安全
        /// </summary>
        bool IsThreadSafe { get; }

        void EnterReadLock();
        void ExitReadLock();

        void EnterWriteLock();
        void ExitWriteLock();
    }
}
