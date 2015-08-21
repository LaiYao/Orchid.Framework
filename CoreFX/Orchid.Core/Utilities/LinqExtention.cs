using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Orchid.Core.Utilities
{
    public static class LinqExtention
    {
        #region | WhereIf |

        public static IQueryable<T> WhereIf<T>([NotNull]this IQueryable<T> source, bool condition, [NotNull]Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        #endregion

        #region | PageBy |

        public static IQueryable<T> PageBy<T>([NotNull]this IQueryable<T> source, int pageIndex, int pageSize)
        {
            Check.NotNull(source, nameof(source));

            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        #endregion

        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
