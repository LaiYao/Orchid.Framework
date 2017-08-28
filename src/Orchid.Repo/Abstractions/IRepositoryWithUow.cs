using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Repo.Abstractions
{
    public interface IRepositoryWithUow<T> : IRepository<T>
    {
        void Add(T value, bool autoCommit);

        void Remove(T value, bool autoCommit);

        void Update(T value, bool autoCommit);

        void Commit();
        void Rollback();
    }
}
