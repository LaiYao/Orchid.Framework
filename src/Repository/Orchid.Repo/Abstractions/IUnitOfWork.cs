using System.Threading.Tasks;

namespace Orchid.Repo.Abstractions
{
    public interface IUnitOfWork
    {
        bool IsCommited { get; }

        void Commit();
        Task CommitAsync();

        void Rollback();
        Task RollbackAsync();
    }
}
