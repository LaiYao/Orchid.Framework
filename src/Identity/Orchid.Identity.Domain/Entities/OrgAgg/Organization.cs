using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.DDD.Domain;

namespace Orchid.Identity.Domain.Entities
{
    public class Organization : AggregateRoot
    {
        public OrganizationType OrganizationType { get; set; }

        public Organization Parent { get; set; }

        #region | Methods |

        public bool IsRootOrganization()
        {
            return Parent == null;
        }



        #endregion
    }

    public enum OrganizationType
    {
        Company = 0,
        BranchCompany = 1,
        Department = 2,
        Group=3
    }
}
