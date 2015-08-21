using System.Threading.Tasks;

namespace Orchid.Repo.Contracts
{
    public interface IUnitOfWork
    {
        bool IsCommited { get; }

        void Commit();
        Task CommitAsync();
    }
}
