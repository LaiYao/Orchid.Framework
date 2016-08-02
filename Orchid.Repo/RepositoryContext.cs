using System.Threading.Tasks;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo
{
    public abstract class RepositoryContext : IRepositoryContext
    {
        public bool IsCommited
        {
            get;
            protected set;
        }

        public abstract void RegisterNew<T>(T value) where T : class;

        public abstract void RegisterModified<T>(T value) where T : class;

        public abstract void RegisterDeleted<T>(T value) where T : class;

        public abstract void Commit();
        public virtual Task CommitAsync() => Task.Run(() => Commit());

        public abstract void Rollback();
        public virtual Task RollbackAsync() => Task.Run(() => Rollback());

        public abstract void Dispose();
    }
}
