using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using Orchid.Core.Utilities;
using Orchid.Repo.Contracts;

namespace Orchid.Repo
{
    public abstract class Repository<T> : IRepository<T>
        where T : class, new()
    {
        #region | Properties |

        IRepositoryContext _Context;
        public IRepositoryContext Context
        {
            get { return _Context; }
            private set
            {
                if (_Context != value)
                {
                    _Context = value;
                }
            }
        }

        public IEnumerable<T> AllItems
        {
            get { return FindAll(); }
        }

        #endregion

        #region | Ctor |

        protected Repository([NotNull] IRepositoryContext repositoryContext)
        {
            Check.NotNull(repositoryContext, nameof(repositoryContext));

            _Context = repositoryContext;
        }

        #endregion

        #region | Members of IRepository |

        public void Add(T value, bool isAutoSave = true)
        {
            _Context.RegisterNew(value);
            if (isAutoSave) _Context.Commit();
        }

        public virtual void Remove(T value, bool isAutoSave = true)
        {
            _Context.RegisterDeleted(value);
            if (isAutoSave) _Context.Commit();
        }

        public virtual void Update(T value, bool isAutoSave = true)
        {
            _Context.RegisterModified(value);
            if (isAutoSave) _Context.Commit();
        }

        public virtual bool Any(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual PagingResult<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<PagingResult<T>> FindAsync<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
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

        public virtual PagingResult<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual Task<PagingResult<T>> FindAllAsync<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, int countPerPage = 10)
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

            Context?.Dispose();

            this.Context = null;
        }

        #endregion
    }
}
