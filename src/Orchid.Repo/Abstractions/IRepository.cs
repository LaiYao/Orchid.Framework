using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orchid.Repo.Abstractions
{
    public interface IRepository<T>
    {
        IQueryable<T> AllItems { get; }

        void Add(T value, bool isAutoCommit = true);

        void Remove(T value, bool isAutoCommit = true);

        void Update(T value, bool isAutoCommit = true);

        bool Any(Expression<Func<T, bool>> cretiria);
        Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria);

        IQueryable<T> Find(Expression<Func<T, bool>> cretiria);
        Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> cretiria);

        /// <summary>
        /// 返回分页过的查询结果
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IPagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
        /// <summary>
        /// 返回分页过的查询结果的异步实现
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns>
        /// </returns>
        Task<IPagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);

        IQueryable<T> FindAll();
        Task<IQueryable<T>> FindAllAsync();

        /// <summary>
        /// 返回分页过的结果
        /// </summary>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IPagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
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
        Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
    }
}
