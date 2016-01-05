using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork.Core.Contracts;

namespace Orchid.SeedWork.Core
{
    public class Logger : ILogger
    {
        public void WriteMessage(string message, LogRate rate, string group = "")
        {
            throw new NotImplementedException();
        }
    }
}
