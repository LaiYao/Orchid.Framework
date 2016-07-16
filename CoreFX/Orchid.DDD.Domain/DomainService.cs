using Orchid.Core.Utilities;
using Orchid.Core.Validation;
using Orchid.Repo.Abstractions;

namespace Orchid.DDD.Domain
{
    public abstract class DomainService<TEntity, TKey> : IDomainService<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        #region | Fields |

        private readonly IRepository<TEntity> _repository;
        protected IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        #endregion

        #region | Ctor |

        public DomainService(IRepository<TEntity> repo)
        {
            Check.NotNull(repo, nameof(repo));

            _repository = repo;
        }

        #endregion

        #region | Members of IDomainService |

        public abstract ValidationResult Add(IEntity<TKey> entity, bool isAutoCommit = true);

        public abstract ValidationResult Remove(IEntity<TKey> entity, bool isAutoCommit = true);

        public abstract ValidationResult Update(IEntity<TKey> entity, bool isAutoCommit = true);

        #endregion
    }
}
