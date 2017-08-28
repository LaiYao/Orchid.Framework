using Orchid.Cloud.Service.Client.Abstractions;
using Orchid.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Orchid.Cloud.Service.Client
{
    public static class InvocationExtensions
    {
        public static string GetUniqueInvocationFieldName(MethodInfo method)
            => $"__invocation_{method.Name}_{method.GetHashCode()}";

        public static IInvocation GetInvocationViaUniqueFieldName(IEnumerable<IInvocation> invocations, string uniqueFieldName)
        {
            Check.NotNull(invocations, nameof(invocations));
            Check.NotNull(uniqueFieldName, nameof(uniqueFieldName));

            if (!uniqueFieldName.StartsWith("__invocation_"))
            {
                throw new ArgumentException("The parameter named 'uniqueFieldName' should be start with '__invocation_'.");
            }

            var methodInfo = uniqueFieldName.Substring("__invocation_".Length);
            var seperatorIndex = methodInfo.LastIndexOf("_");
            var methodName = methodInfo.Substring(0, seperatorIndex);
            var methodHashCode = methodInfo.Substring(seperatorIndex + 1);

            return invocations.SingleOrDefault(_ => _.Method.Name == methodName && int.Parse(methodHashCode) == _.Method.GetHashCode());
        }
    }
}
