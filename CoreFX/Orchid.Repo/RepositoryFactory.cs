using System;
using Orchid.Repo.Contracts;

namespace Orchid.Repo
{
    public abstract class RepositoryFactory : IRepositoryFactory
    {
        #region | IRepositoryFactory |

        public abstract IRepository<T> Create<T>();

        #endregion
    }
}
