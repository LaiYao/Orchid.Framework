using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Utilities
{
    public static class LinqExtenions
    {
        public static void Foreach<T>(this IEnumerable<T> list, Expression<Action<T>> expression)
        {
            var action = expression.Compile();
            foreach (var item in list)
            {
                action.Invoke(item);
            }
        }
    }
}
