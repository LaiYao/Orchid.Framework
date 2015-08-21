using System.Threading.Tasks;
using Orchid.Repo.Contracts;

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
        public abstract Task CommitAsync();

        public abstract void Dispose();
    }
}
