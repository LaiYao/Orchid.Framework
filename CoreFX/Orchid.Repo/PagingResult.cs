using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Repo.Abstractions;

namespace Orchid.Repo
{
    public class PagingResult<T> : IPagingResult<T> where T : class, new()
    {
        public IQueryable<T> Items { get; set; }

        public long ItemsCount { get; set; }

        public int PagesCount { get; set; }

        public PagingResult(IQueryable<T> items, long itemsCount, int pagesCount)
        {
            Items = items;
            ItemsCount = itemsCount;
            PagesCount = pagesCount;
        }
    }
}
