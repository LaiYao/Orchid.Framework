using Orchid.SeedWork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.Tool.Generic
{
    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        #region | Fields |

        readonly Func<T, T, bool> _lambdaCompare;

        #endregion

        #region | Properties |



        #endregion

        #region | Ctor |

        public LambdaComparer(Func<T, T, bool> lambdaCompare)
        {
            ExceptionUtilities.RequireNotNullChecker(lambdaCompare, "lambdaCompare");
        }

        #endregion

        #region | Overrides |



        #endregion

        #region | IEqualityComparer |

        public bool Equals(T x, T y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

