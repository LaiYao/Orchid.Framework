using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Repo.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> AllItems { get; }

        void Add(T value);

        void Remove(T value);

        void Update(T value);

        bool Any(Func<T, bool> cretiria);
        Task<bool> AnyAsync(Func<T, bool> cretiria);

        IEnumerable<T> Find(Func<T, bool> cretiria);
        Task<IEnumerable<T>> FindAsync(Func<T, bool> cretiria);

        /// <summary>
        /// 返回分页过的查询结果
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IPagingResult<T> Find<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10);
        /// <summary>
        /// 返回分页过的查询结果的异步实现
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns>
        /// </returns>
        Task<IPagingResult<T>> FindAsync<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10);

        IEnumerable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 返回分页过的结果
        /// </summary>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IPagingResult<T> FindAll<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10);
        /// <summary>
        /// 返回分页过的结果
        /// </summary>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="itemsCount">总条目数</param>
        /// <param name="pagesCount">总页数</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns>
        /// </returns>
        Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10);
    }
}
