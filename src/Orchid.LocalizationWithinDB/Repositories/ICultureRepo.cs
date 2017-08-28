using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.LocalizationWithinDB.Entities;
using Orchid.Repo.Abstractions;

namespace Orchid.LocalizationWithinDB.Repositories
{
    public interface ICultureRepo : IRepository<Culture>
    {
    }
}
