using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository.Contracts
{
    public interface IRepositoryFactory : IDisposable
    {
        IRepository<T> CreateRepository<T>() where T : class;
    }
}
