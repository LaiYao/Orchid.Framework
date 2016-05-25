using System;

namespace Orchid.Repo.Abstractions
{
    public interface IRepositoryFactory<TContext> : IDisposable where TContext:IRepositoryContext
    {
        TContext Context { get; }

        IRepository<T> Create<T>();
    }
}
