using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Framework.Internal;

namespace Orchid.Core.Utilities
{
    public static class LinqExtensions
    {
        #region | WhereIf |

        public static IEnumerable<T> WhereIf<T>([NotNull]this IEnumerable<T> source, bool condition, [NotNull]Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        #endregion

        #region | PageBy |

        public static IEnumerable<T> PageBy<T>([NotNull]this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        #endregion

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> source,Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }

        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
