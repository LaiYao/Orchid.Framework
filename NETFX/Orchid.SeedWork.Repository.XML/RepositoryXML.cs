using Orchid.SeedWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Orchid.SeedWork.Repository.XML
{
    public class RepositoryXML<T> : RepositoryBase<T> where T : class
    {
        #region | Fields |

        #endregion

        #region | Properties |

        #region | Selector |

        protected virtual Func<XElement, T> Selector { get; set; }

        #endregion

        #endregion

        #region | Ctor |

        public RepositoryXML(RepositoryContextXML context)
            : base(context)
        {
            ExceptionUtilities.RequireNotNullChecker(context, "context");
        }

        #endregion

        #region | Overrides |



        #endregion

        #region | Methods |



        #endregion
    }
}
