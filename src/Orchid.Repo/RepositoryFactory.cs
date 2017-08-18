using System;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo
{
    public abstract class RepositoryFactory<TContext> : IRepositoryFactory<TContext> where TContext : IRepositoryContext
    {
        #region | IRepositoryFactory |

        public TContext Context { get; protected set; }

        public abstract IRepository<T> Create<T>();

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

            this.Context = default(TContext);
        }

        #endregion
    }
}
