using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Identity.Domain.Entities
{
    public class User
    {
        public string Number { get; set; }

        public string Name { get; set; }

        public string RealName { get; set; }

        public string NickName { get; set; }

        public string EmailAddress { get; set; }

        public string MobilePhone { get; set; }

        public string QQ { get; set; }

        public bool IsDelete { get; set; }

        public bool IsExpire { get; set; }

        public IList<Organization> OrgList { get; set; }
    }
}
