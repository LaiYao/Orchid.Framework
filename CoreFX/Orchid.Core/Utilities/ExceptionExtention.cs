using System;

namespace Orchid.Core.Utilities
{
    public static class ExceptionExtention
    {
        public static void LogAndThrow(this Exception exception)
        {   
            throw exception;
        }
    }
}
