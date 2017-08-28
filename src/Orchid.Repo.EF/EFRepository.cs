using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orchid.Repo.Abstractions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore.Query;

namespace Orchid.Repo.EF
{
    public class EFRepository<T> : RepositoryWithUow<T, EFRepositoryContext> where T : class, new()
    {
        #region | Ctor |

        public EFRepository(EFRepositoryContext context)
            : base(context)
        {
        }

        #endregion

        public override bool Any(Func<T, bool> cretiria)
            => Context.Context.Set<T>().Any(cretiria);

        public override async Task<bool> AnyAsync(Func<T, bool> cretiria)
            => await Context.Context.Set<T>().AnyAsync();

        public override IEnumerable<T> Find(Func<T, bool> cretiria)
            => Context.Context.Set<T>().Where(cretiria);

        public override async Task<IEnumerable<T>> FindAsync(Func<T, bool> cretiria)
            => await Task.FromResult(Find(cretiria));

        public override IPagingResult<T> Find<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = Context.Context.Set<T>().Where(cretiria).Count();
            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            var items = Context.Context.Set<T>().Where(cretiria).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

            return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAsync<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(Find(cretiria, orderBy, pageIndex, countPerPage));

        public override IEnumerable<T> FindAll() => Context.Context.Set<T>();

        public override async Task<IEnumerable<T>> FindAllAsync()
            => await Task.FromResult(FindAll());

        public override IPagingResult<T> FindAll<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = Context.Context.Set<T>().Count();
            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            var items = Context.Context.Set<T>().OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

            return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(FindAll(orderBy, pageIndex, countPerPage));
    }
}
