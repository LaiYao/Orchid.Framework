using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Orchid.Core.Utilities;
using Orchid.Repo.Abstractions;
using System.Collections.Generic;

namespace Orchid.Repo
{
    public abstract class Repository<T> : IRepository<T>, IDisposable
        where T : class, new()

    {
        #region | Properties |

        public IEnumerable<T> AllItems
        {
            get { return FindAll(); }
        }

        #endregion

        #region | IRepository |

        public virtual void Add(T value)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(T value)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T value)
        {
            throw new NotImplementedException();
        }

        public virtual bool Any(Func<T, bool> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AnyAsync(Func<T, bool> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Find(Func<T, bool> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindAsync(Func<T, bool> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IPagingResult<T> Find<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IPagingResult<T>> FindAsync<TOrderKey>(Func<T, bool> cretiria, Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual IPagingResult<T> FindAll<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IPagingResult<T>> FindAllAsync<TOrderKey>(Func<T, TOrderKey> orderBy, int pageIndex, int countPerPage = 10)
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
        }

        #endregion
    }

    public abstract class RepositoryWithUow<T, TContext> : Repository<T>, IRepositoryWithUow<T>
        where T : class, new()
        where TContext : IRepositoryContext
    {
        #region | Properties |

        protected TContext Context { get; private set; }

        #endregion

        #region | Ctor |

        protected RepositoryWithUow(TContext repositoryContext)
        {
            Check.NotNull(repositoryContext, nameof(repositoryContext));

            Context = repositoryContext;
        }

        #endregion


        #region | IRepositoryWithUow |

        public override void Add(T value)
        {
            Add(value, true);
        }

        public virtual void Add(T value, bool autoCommit)
        {
            Context.RegisterNew(value);
            if (autoCommit)
            {
                Commit();
            }
        }

        public override void Remove(T value)
        {
            Remove(value, true);
        }

        public virtual void Remove(T value, bool autoCommit)
        {
            Context.RegisterDeleted(value);
            if (autoCommit)
            {
                Commit();
            }
        }

        public override void Update(T value)
        {
            Update(value, true);
        }

        public virtual void Update(T value, bool autoCommit)
        {
            Context.RegisterModified(value);
            if (autoCommit)
            {
                Commit();
            }
        }

        public virtual void Commit()
        {
            Context.Commit();
        }

        public virtual void Rollback()
        {
            Context.Rollback();
        }

        #endregion

        #region | IDispose |

        public override void Dispose(bool disposing)
        {
            if (!disposing) return;

            base.Dispose(disposing);
            // Dispose的时候，不需要调用Context.Dispose方法，因为Context通常是多个Repository共享
            this.Context = default(TContext);
        }

        #endregion
    }
}
