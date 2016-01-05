using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Cache
{
    public static class CacheFactory
    {
        static ICacheFactory _factory = null;

        public static void SetCurrent(ICacheFactory factory)
        {
            _factory = factory;
        }

        public static ICache Create()
        {
            return _factory == null ? null : _factory.Create();
        }
    }
}
