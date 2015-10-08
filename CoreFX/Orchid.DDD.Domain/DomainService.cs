using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Utilities;
using Orchid.Core.Validation;
using Orchid.Repo.Contracts;

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

        public abstract ValidationResult Add(IEntity<TKey> entity, bool isAutoSave = true);

        public abstract ValidationResult Remove(IEntity<TKey> entity, bool isAutoSave = true);

        public abstract ValidationResult Update(IEntity<TKey> entity, bool isAutoSave = true);

        #endregion
    }
}
