using Orchid.SeedWork.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository
{
    public abstract class RepositoryFactoryBase : IRepositoryFactory
    {
        protected IRepositoryContext _context;

        public RepositoryFactoryBase(IRepositoryContext context)
        {
            _context = context;
        }

        public abstract IRepository<T> CreateRepository<T>() where T : class;

        #region | IDispose |

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_context == null) return;

            _context.Dispose();

            this._context = null;
        }

        #endregion
    }
}
