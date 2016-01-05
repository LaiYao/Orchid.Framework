using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork;
using Orchid.SeedWork.Core;
using Orchid.SeedWork.Core.Contracts;
using Orchid.SeedWork.DDD.Domain.Contracts;

namespace Orchid.SeedWork.DDD.Domain
{
    public abstract class DomainEntity : IEntity, IIdentifiable<Guid>, ILifeTraceable
    {
        #region | Fields |

        int? _requestedHashCode;

        #endregion

        #region | Members of IIdentifiable |

        #region | ID |

        private Guid _ID;
        public virtual Guid ID
        {
            get { return _ID; }
            set
            {
                if (value == _ID)
                    return;
                _ID = value;
            }
        }

        #endregion

        #endregion

        #region | Members of ILifeTrace |

        #region | IsNew |

        private bool _IsNew = true;
        public bool IsNew
        {
            get { return _IsNew; }
            set
            {
                if (value == _IsNew)
                    return;
                _IsNew = value;
            }
        }

        #endregion

        #region | IsDirty |

        private bool _IsDirty = false;
        public bool IsDirty
        {
            get { return _IsDirty; }
            set
            {
                if (value == _IsDirty)
                    return;
                _IsDirty = value;
            }
        }

        #endregion

        #region | IsDelete |

        private bool _IsDelete = false;
        public bool IsDelete
        {
            get { return _IsDelete; }
            set
            {
                if (value == _IsDelete)
                    return;
                _IsDelete = value;
            }
        }

        #endregion

        #endregion

        #region | Public Methods |

        public bool IsTransient()
        {
            return this.ID == Guid.Empty;
        }

        public void GenerateNewIdentity()
        {
            if (IsTransient()) this.ID = SequentialGuid.NewSequentialGuid();
        }

        public void ChangeCurrentIdentity(Guid key)
        {
            if (key != Guid.Empty) this.ID = key;
        }

        #endregion

        #region | Override Methods |

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is DomainEntity)) return false;

            if (Object.ReferenceEquals(this, obj)) return true;

            var entity = (DomainEntity)obj;
            if (this.IsTransient() || entity.IsTransient()) return false;
            else return this.ID == entity.ID;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue) _requestedHashCode = this.ID.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else return base.GetHashCode();
        }

        public static bool operator ==(DomainEntity left, DomainEntity right)
        {
            if (Object.Equals(left, null)) return (Object.Equals(right, null)) ? true : false;
            else return left.Equals(right);
        }

        public static bool operator !=(DomainEntity left, DomainEntity right)
        {
            return !(left == right);
        }

        #endregion
    }
}
