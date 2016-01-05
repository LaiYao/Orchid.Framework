using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Logger
{
    public static class LoggerFactory
    {
        static ILoggerFactory _factory = null;

        public static void SetCurrent(ILoggerFactory factory)
        {
            _factory = factory;
        }

        public static ILogger Create()
        {
            return _factory == null ? null : _factory.Create();
        }
    }
}
