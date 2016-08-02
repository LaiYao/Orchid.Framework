using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Identity.Domain.Entities;
using Orchid.Repo.Abstractions;

namespace Orchid.Identity.Domain.Repositories
{
    public interface IUserRepo : IRepository<User>
    {
        string GetUserById(string id);
    }
}
