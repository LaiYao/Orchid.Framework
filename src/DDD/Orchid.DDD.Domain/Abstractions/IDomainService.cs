using Orchid.Core.Validation;

namespace Orchid.DDD.Domain
{
    public interface IDomainService<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        ValidationResult Add(IEntity<TKey> entity, bool isAutoCommit = true);

        ValidationResult Update(IEntity<TKey> entity, bool isAutoCommit = true);

        ValidationResult Remove(IEntity<TKey> entity, bool isAutoCommit = true);
    }
}
