using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository.Contracts
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IRepositoryContext Context { get; }

        void Add(T value, bool isSave = true);

        bool Any(Expression<Func<T, bool>> cretiria);

        IQueryable<T> Find(Expression<Func<T, bool>> cretiria);

        /// <summary>
        /// 返回分页过的查询结果
        /// </summary>
        /// <param name="cretiria">Lambda表达式表示的查询条件</param>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="itemsCount">总条目数</param>
        /// <param name="pagesCount">总页数</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IQueryable<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10);

        IQueryable<T> FindAll();

        /// <summary>
        /// 返回分页过的结果
        /// </summary>
        /// <param name="orderBy">Lambda表达式表示的排序条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="itemsCount">总条目数</param>
        /// <param name="pagesCount">总页数</param>
        /// <param name="countPerPage">每页条目数，默认为10条</param>
        /// <returns></returns>
        IQueryable<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10);

        //T GetByID(object key);

        void Remove(T value, bool isSave = true);

        void Update(T value, bool isSave = true);
    }
}
