using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Utilities;

namespace Orchid.Core.Extensions
{
    public static class EqualityComparerExtensions
    {
        public static IEqualityComparer<T> GetDefaltComparer<T>(this T ob, Func<T, T, bool> comparisonFunction)
        {
            return new DefaultCompare<T>(comparisonFunction);
        }
    }

    public class DefaultCompare<T> : IEqualityComparer<T>
    {
        Func<T, T, bool> _comparisonFunction;

        public DefaultCompare(Func<T, T, bool> comparisonFunction)
        {
            Check.NotNull(comparisonFunction, nameof(comparisonFunction));
            _comparisonFunction = comparisonFunction;
        }

        public bool Equals(T x, T y)
        {
            return _comparisonFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
