using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Internal;
using Orchid.Core.Utilities;
using Orchid.DDD.Domain;

namespace Orchid.Permission.Domain
{
    public class Role : Entity<int>, IAggregateRoot
    {
        public int Version
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public RoleCategory RoleCategory { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        #region | Ctor |

        public Role()
        {
        }

        public Role([NotNull]RoleCategory roleCategory)
        {
            Check.NotNull(roleCategory, nameof(roleCategory));

            RoleCategory = roleCategory;
        }

        #endregion
    }
}
