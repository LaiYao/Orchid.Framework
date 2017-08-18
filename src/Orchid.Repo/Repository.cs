using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orchid.Core.Utilities;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo
{
    public abstract class Repository<T, TContext> : IRepository<T>, IDisposable
        where T : class, new()
        where TContext : IRepositoryContext
    {
        #region | Properties |

        protected TContext Context { get; private set; }

        public IQueryable<T> AllItems
        {
            get { return FindAll(); }
        }

        #endregion

        #region | Ctor |

        protected Repository(TContext repositoryContext)
        {
            Check.NotNull(repositoryContext, nameof(repositoryContext));

            Context = repositoryContext;
        }

        #endregion

        #region | Members of IRepository |

        public void Add(T value, bool isAutoCommit = true)
        {
            Context.RegisterNew(value);
            if (isAutoCommit) Context.Commit();
        }

        public virtual void Remove(T value, bool isAutoCommit = true)
        {
            Context.RegisterDeleted(value);
            if (isAutoCommit) Context.Commit();
        }

        public virtual void Update(T value, bool isAutoCommit = true)
        {
            Context.RegisterModified(value);
            if (isAutoCommit) Context.Commit();
        }

        public virtual bool Any(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IPagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IPagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IQueryable<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual IPagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region | IDispose |

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            this.Context = default(TContext);
        }

        #endregion
    }
}
