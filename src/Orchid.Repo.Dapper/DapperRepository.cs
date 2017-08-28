using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Orchid.Repo;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo.Dapper
{
    public class DapperRepository<T> : RepositoryWithUow<T, DapperRepositoryContext> where T : class, new()
    {
        #region | Ctor |

        public DapperRepository(DapperRepositoryContext context)
            : base(context)
        {
        }

        #endregion

        public override bool Any(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
            //return Context.Connection.Get<T>().Any(cretiria);
        }

        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria) => await Task.FromResult(Any(cretiria));

        public override IEnumerable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            return Context.Context.Set<T>().Where(cretiria);
        }

        public override async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
            => await Task.FromResult(Find(cretiria));

        public override IPagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
            //if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            //if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            //var itemsCount = Context.Context.Set<T>().Where(cretiria).Count();
            //var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            //var items = Context.Context.Set<T>().Where(cretiria).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

            //return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(Find(cretiria, orderBy, pageIndex, countPerPage));

        public override IEnumerable<T> FindAll() => Context.Connection.GetAll<T>();

        public override async Task<IEnumerable<T>> FindAllAsync()
            => await Task.FromResult(FindAll());

        public override IPagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
            //if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            //if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            //var itemsCount = Context.Context.Set<T>().Count();
            //var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            //var items = Context.Context.Set<T>().OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

            //return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(FindAll(orderBy, pageIndex, countPerPage));
    }
}
