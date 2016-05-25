using System;
using System.Linq;
using System.Linq.Expressions;
// using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo.EF
{
    public class RepositoryEF<T> : Repository<T, RepositoryContextEF> where T : class, new()
    {
        #region | Ctor |

        public RepositoryEF(RepositoryContextEF context)
            : base(context)
        {
        }

        #endregion

        public override bool Any(Expression<Func<T, bool>> cretiria)
        {
            return Context.Context.Set<T>().Any(cretiria);
        }

        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria) => await Task.FromResult(Any(cretiria));

        public override IQueryable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            return Context.Context.Set<T>().Where(cretiria);
        }

        public override async Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
            => await Task.FromResult(Find(cretiria));

        public override IPagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = Context.Context.Set<T>().Where(cretiria).Count();
            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            var items = Context.Context.Set<T>().Where(cretiria).OrderBy(orderBy).Skip(()=>pageIndex * countPerPage).Take(()=>countPerPage);

            return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(Find(cretiria, orderBy, pageIndex, countPerPage));

        public override IQueryable<T> FindAll() => Context.Context.Set<T>();

        public override async Task<IQueryable<T>> FindAllAsync()
            => await Task.FromResult(FindAll());

        public override IPagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

            var itemsCount = Context.Context.Set<T>().Count();
            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

            var items = Context.Context.Set<T>().OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

            return new PagingResult<T>(items, itemsCount, pagesCount);
        }

        public override async Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
            => await Task.FromResult(FindAll(orderBy, pageIndex, countPerPage));

        // /// <summary>
        // /// 批量删除的SQL转换版本，用于优化性能
        // /// </summary>
        // /// <param name="cretiria">筛选条件</param>
        // public virtual void RemoveItems(Expression<Func<T, bool>> cretiria = null, bool isAutoCommit = true)
        // {
        //     var set = _Context.Context.Set<T>().AsQueryable();
        //     set = (cretiria == null) ? set : set.Where(cretiria);

        //     string sql = set.ToString().Replace("\r", "").Replace("\n", "").Trim();
        //     if (cretiria == null && !string.IsNullOrEmpty(sql) && !string.IsNullOrWhiteSpace(sql))
        //         sql += " WHERE 1=1";

        //     Regex reg = new Regex("^SELECT[\\s]*(?<Fields>.*)[\\s]*FROM[\\s]*(?<Table>.*)[\\s]*AS[\\s]*(?<TableAlias>.*)[\\s]*WHERE[\\s]*(?<Condition>.*)", RegexOptions.IgnoreCase);
        //     Match match = reg.Match(sql);

        //     if (!match.Success)
        //         throw new ArgumentException("Cannot delete this type of collection");

        //     string table = match.Groups["Table"].Value.Trim();
        //     string tableAlias = match.Groups["TableAlias"].Value.Trim();
        //     string condition = match.Groups["Condition"].Value.Trim().Replace(tableAlias, table);

        //     string sql1 = string.Format("DELETE FROM {0} WHERE {1}", table, condition);

        //     _Context.Context.Database.BeginTransaction();
        //     _Context.Context.Database.ExecuteSqlCommand(sql1);
        // }
    }
}
