//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Orchid.Repo.Abstractions;
//using MongoDB.Driver;
//using Orchid.Core.Utilities;

//namespace Orchid.Repo.Mongo
//{
//    public class MongoRepository<T> : Repository<T, MongoRepositoryContext> where T : class, new()
//    {
//        #region | Fields |

//        IMongoCollection<T> _collection;

//        #endregion

//        #region | Ctor |

//        public MongoRepository(MongoRepositoryContext context, string collectName)
//            : base(context)
//        {
//            Check.NotNull(context, nameof(context));
//            Check.NotNull(collectName, nameof(collectName));

//            _collection = Context.DB.GetCollection<T>(collectName);
//        }

//        #endregion

//        public override bool Any(Expression<Func<T, bool>> cretiria)
//        {
//            return _collection.FindSync(cretiria).Any();
//        }

//        public override async Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria)
//        {
//            var asyncResult = await _collection.FindAsync(cretiria);
//            return asyncResult.Any();
//        }

//        public override IEnumerable<T> Find(Expression<Func<T, bool>> cretiria)
//        {
//            return _collection.FindSync(cretiria).ToList();
//        }

//        public override async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
//        {
//            var asyncResult = await _collection.FindAsync(cretiria);
//            return asyncResult.ToList();
//        }

//        public override IPagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
//        {
//            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
//            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

//            var itemsCount = _collection.Count(cretiria);
//            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);


//            var items = _collection.FindSync(cretiria).order.OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

//            return new PagingResult<T>(items, itemsCount, pagesCount);
//        }

//        public override async Task<IPagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
//            => await Task.FromResult(Find(cretiria, orderBy, pageIndex, countPerPage));

//        public override IEnumerable<T> FindAll() => _collection.FindSync(null).ToList();

//        public override async Task<IEnumerable<T>> FindAllAsync()
//            => await _collection.FindAsync(null).Result.ToListAsync();

//        public override IPagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
//        {
//            if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
//            if (countPerPage < 1) throw new ArgumentOutOfRangeException(nameof(countPerPage));

//            var itemsCount = _collection.Count(null);
//            var pagesCount = (int)Math.Ceiling((decimal)itemsCount / countPerPage);

//            var items = _collection.FindSync(null).OrderBy(orderBy).Skip(pageIndex * countPerPage).Take(countPerPage);

//            return new PagingResult<T>(items, itemsCount, pagesCount);
//        }

//        public override async Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
//            => await Task.FromResult(FindAll(orderBy, pageIndex, countPerPage));
//    }
//}
