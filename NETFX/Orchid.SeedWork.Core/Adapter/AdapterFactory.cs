using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Adapter
{
    public class AdapterFactory
    {
        static IAdapterFactory _factory = null;

        public static void SetCurrent(IAdapterFactory factory)
        {
            _factory = factory;
        }

        public static IAdapter Create()
        {
            return _factory == null ? null : _factory.Create();
        }
    }
}
