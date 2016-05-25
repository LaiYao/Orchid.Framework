using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orchid.Core.Utilities
{
    public static class Check
    {
        public static T NotNull<T>(
            T value,
            string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T NotNull<T>(
            T value,
            [NotNull] string parameterName,
            [NotNull] string propertyName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));

                throw new ArgumentException($"Argument '{parameterName}' is NULL.");
            }

            return value;
        }

        public static IReadOnlyList<T> NotEmpty<T>(
            IReadOnlyList<T> value,
            [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"Collection '{parameterName}' is EMPTY.");
            }

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>(
            IEnumerable<T> value,
            [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count() == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"Collection '{parameterName}' is EMPTY.");
            }

            return value;
        }

        public static string NotEmpty(
            string value,
            [NotNull] string parameterName)
        {
            Exception e = null;
            if (ReferenceEquals(value, null))
            {
                e = new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                e = new ArgumentException($"Argument '{parameterName}' is empty.");
            }

            if (e != null)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw e;
            }

            return value;
        }

        public static string NullButNotEmpty(
            string value,
            [NotNull] string parameterName)
        {
            if (!ReferenceEquals(value, null)
                && value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"parameter '{parameterName}' is not NULL but EMPTY.");
            }

            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(
            IReadOnlyList<T> value, 
            [NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static T IsDefined<T>(
            T value, 
            [NotNull] string parameterName)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"parameter '{parameterName}' is not defined.");
            }

            return value;
        }

        public static Type ValidEntityType(
            Type value, 
            [NotNull] string parameterName)
        {
            if (!value.GetTypeInfo().IsClass)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"the type of parameter '{parameterName}' is not a class.");
            }

            return value;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter |
                    AttributeTargets.Property | AttributeTargets.Delegate |
                    AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter |
                    AttributeTargets.Property | AttributeTargets.Delegate |
                    AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }
}
