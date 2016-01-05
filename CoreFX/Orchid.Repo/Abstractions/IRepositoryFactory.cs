using System;

namespace Orchid.Repo.Abstractions
{
    public interface IRepositoryFactory
    {
        IRepository<T> Create<T>();
    }
}
