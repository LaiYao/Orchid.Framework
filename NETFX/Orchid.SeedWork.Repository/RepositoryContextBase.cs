using Orchid.SeedWork.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Repository
{
    public abstract class RepositoryContextBase : IRepositoryContext
    {
        readonly Guid _ID = new Guid();
        public Guid ID
        {
            get { return _ID; }
        }

        public bool IsCommited
        {
            get;
            protected set;
        }

        public virtual void RegisterNew<T>(T value) where T : class { }

        public virtual void RegisterModified<T>(T value) where T : class { }

        public virtual void RegisterDeleted<T>(T value) where T : class { }

        public virtual void Commit() { }

        public virtual void Rollback() { }

        public virtual void Dispose() { }
    }
}
