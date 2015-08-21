using System;
using System.Threading.Tasks;

namespace Orchid.Repo.Contracts
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        void RegisterNew<T>(T value) where T : class;

        void RegisterModified<T>(T value) where T : class;

        void RegisterDeleted<T>(T value) where T : class;
    }
}
