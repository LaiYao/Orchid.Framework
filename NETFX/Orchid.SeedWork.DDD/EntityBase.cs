using Orchid.SeedWork.DDD.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.DDD
{
    public abstract class EntityBase : IEntity
    {
        #region | Properties |

        #region | Key |

        private object _Key;
        public object Key
        {
            get { return _Key; }
        }

        #endregion

        #endregion

        #region | Ctor |

        protected EntityBase()
            : this(null)
        {

        }

        protected EntityBase(object key)
        {
            _Key = key;
        }

        #endregion

        #region | Overrides |

        public override bool Equals(object entity)
        {
            if (entity == null || !(entity is EntityBase))
            {
                return false;
            }
            return this == (EntityBase)entity;
        }

        public static bool operator ==(EntityBase entity1, EntityBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            if (entity1.Key != entity2.Key)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }

        public override int GetHashCode()
        {
            if (this.Key != null)
            {
                return this.Key.GetHashCode();
            }

            return 0;
        }

        #endregion

    }
}
