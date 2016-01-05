using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository.Contracts
{
    public interface IUnitOfWork
    {
        bool IsCommited { get; }


        void Commit();

        void Rollback();
    }
}
