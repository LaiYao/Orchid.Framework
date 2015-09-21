using System;

namespace Orchid.Core.Utilities
{
    public static class ExceptionExtensions
    {
        public static void LogAndThrow(this Exception exception)
        {   
            throw exception;
        }
    }
}
