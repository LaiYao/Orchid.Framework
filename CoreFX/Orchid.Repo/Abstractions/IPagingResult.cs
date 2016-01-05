using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Repo.Abstractions
{
    public interface IPagingResult<T>
    {
        IEnumerable<T> Items { get; }

        long ItemsCount { get; }

        int PagesCount { get; }
    }
}
