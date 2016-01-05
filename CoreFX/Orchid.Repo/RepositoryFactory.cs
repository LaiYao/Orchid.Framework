using System;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo
{
    public abstract class RepositoryFactory : IRepositoryFactory
    {
        #region | IRepositoryFactory |

        public abstract IRepository<T> Create<T>();

        #endregion
    }
}
