using System;
using System.Collections.Generic;
using System.Text;

namespace OMP.Domain.Entities
{
    public class Group : BaseEntity, IHaveLables
    {
        public string NamespaceId { get; set; }

        public Dictionary<string, string> Lables { get; set; }

        /// <summary>
        /// 启用的服务
        /// </summary>
        public List<BuiltinService> UsedBuiltinServices { get; set; }

    }
}
