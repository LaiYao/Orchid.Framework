using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core
{
    public static class ExceptionUtilities
    {
        public static void RequireNotNullChecker(object value, string propertyName = "")
        {
            // TODO: Add a lambda version
            if (value == null)
            {
                var exception = new ArgumentNullException(propertyName);

                Trace.TraceError(exception.Message);

                throw exception;
            }
        }

        public static void ArgumentsChecker(bool isPassed, string info)
        {
            if (!isPassed)
            {
                var exception = new ArgumentException(info);

                Trace.TraceError(exception.Message);

                throw exception;
            }
        }
    }
}
