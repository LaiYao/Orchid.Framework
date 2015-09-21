using System;

namespace Orchid.Repo.Contracts
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>() where T : class, new();
    }
}
