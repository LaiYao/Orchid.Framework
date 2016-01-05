using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core.Logger
{
    public interface ILogger
    {
        void LogInfo(string message, params object[] args);
    }
}
