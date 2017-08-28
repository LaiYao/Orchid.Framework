using System.Collections.Generic;
using System.Linq;

namespace Orchid.Repo.Abstractions
{
    public interface IPagingResult<T>
    {
        IEnumerable<T> Items { get; }

        long ItemsCount { get; }

        int PagesCount { get; }
    }
}
