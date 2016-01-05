using Orchid.SeedWork.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
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

        #endregion

        #region | Ctor |

        protected RepositoryBase(IRepositoryContext repositoryContext)
        {
            _Context = repositoryContext;
        }

        #endregion

        #region | Members of IRepository |

        public virtual void Add(T value, bool isSave = true)
        {
            _Context.RegisterNew(value);
            if (isSave) _Context.Commit();
        }

        public virtual void Remove(T value, bool isSave = true)
        {
            _Context.RegisterDeleted(value);
            if (isSave) _Context.Commit();
        }

        public virtual void Update(T value, bool isSave = true)
        {
            _Context.RegisterModified(value);
            if (isSave) _Context.Commit();
        }

        public virtual bool Any(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> cretiria)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> Find<TOrderKey>(Expression<Func<T, bool>> cretiria, Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> FindAll<TOrderKey>(Expression<Func<T, TOrderKey>> orderBy, int pageIndex, out int itemsCount, out int pagesCount, int countPerPage = 10)
        {
            throw new NotImplementedException();
        }

        //public virtual T GetByID(object key)
        //{
        //    throw new NotImplementedException();
        //}

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

            if (Context == null) return;

            Context.Dispose();

            this.Context = null;
        }

        #endregion
    }
}
