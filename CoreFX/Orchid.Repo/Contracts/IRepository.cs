using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Orchid.Repo.Contracts
{
    public interface IRepository<T> : IDisposable where T : class, new()
    {
        IEnumerable<T> AllItems { get; }

        IRepositoryContext Context { get; }

        void Add(T value, bool isAutoSave = true);

        void Remove(T value, bool isAutoSave = true);

        void Update(T value, bool isAutoSave = true);

        bool Any(Expression<Func<T, bool>> cretiria);
        Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria);

        IEnumerable<T> Find(Expression<Func<T, bool>> cretiria);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> cretiria);

        /// <summary>
        /// 返回分页过的查询结果
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        PagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
        /// <summary>
        /// 返回分页过的查询结果的异步实现
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns>
        /// </returns>
        Task<PagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);

        IEnumerable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 返回分页过的结果
        /// </summary>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        PagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
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
        Task<PagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10);
    }
}
