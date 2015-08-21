using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orchid.Repo.Contracts;

namespace Orchid.Repo.EF
{
    public class RepositoryEF<T> : Repository<T> where T : class
    {
        #region | Properties |

        #region | Context |

        protected RepositoryContextEF _Context;
        public new IRepositoryContext Context
        {
            get
            {
                return _Context;
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        public RepositoryEF(RepositoryContextEF context)
            : base(context)
        {
            if (context == null) throw new NullReferenceException(nameof(context));

            _Context = context;
        }

        #endregion

        public override bool Any(Expression<Func<T, bool>> cretiria)
        {
            return _Context.Context.Set<T>().Any(cretiria);
        }

        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria)
        {
            return await Task<bool>.Factory.StartNew(() => _Context.Context.Set<T>().Any(cretiria));
        }

        public new IQueryable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            return _Context.Context.Set<T>().Where(cretiria);
        }

        public new async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
        {
            return await Task<IQueryable<T>>.Factory.StartNew(() => _Context.Context.Set<T>().Where(cretiria));
        }

        public new IQueryable<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));

            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            itemsCount = _Context.Context.Set<T>().Where(cretiria).Count();

            pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            return _Context.Context.Set<T>().Where(cretiria).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);
        }

        public new async Task<Tuple<IQueryable<T>, int, int>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));

            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = await Task<int>.Factory.StartNew(() => _Context.Context.Set<T>().Where(cretiria).Count());

            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            return await Task<Tuple<IQueryable<T>, int, int>>
                .Factory
                .StartNew(
                () =>
                new Tuple<IQueryable<T>, int, int>(
                  _Context.Context.Set<T>().Where(cretiria).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage),
                  itemsCount,
                  pagesCount));
        }

        public new IQueryable<T> FindAll()
        {
            return _Context.Context.Set<T>();
        }

        public new async Task<IQueryable<T>> FindAllAsync()
        {
            return await Task<IQueryable<T>>
                .Factory
                .StartNew(
                () =>
                _Context.Context.Set<T>()
                );
        }

        public new IQueryable<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));

            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            itemsCount = _Context.Context.Set<T>().Count();

            pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            return _Context.Context.Set<T>().OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);
        }

        public new async Task<Tuple<IQueryable<T>, int, int>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));

            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = await Task<int>.Factory.StartNew(() => _Context.Context.Set<T>().Count());

            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            return await Task<Tuple<IQueryable<T>, int, int>>
                .Factory
                .StartNew(
                () =>
                new Tuple<IQueryable<T>, int, int>(
                _Context.Context.Set<T>().OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage),
                itemsCount,
                pagesCount));
        }
    }
}
