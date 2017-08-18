using System.Linq;

namespace Orchid.Repo.Abstractions
{
    public interface IPagingResult<T>
    {
        IQueryable<T> Items { get; }

        long ItemsCount { get; }

        int PagesCount { get; }
    }
}
