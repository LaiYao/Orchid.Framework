using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository.Contracts
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid ID { get; }

        void RegisterNew<T>(T value) where T : class;

        void RegisterModified<T>(T value) where T : class;

        void RegisterDeleted<T>(T value) where T : class;
    }
}
