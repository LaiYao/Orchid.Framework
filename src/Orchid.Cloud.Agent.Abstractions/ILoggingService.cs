using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Agent.Abstractions
{
    public interface ILoggingService
    {
        void Post(LoggingEntry entity);
    }
}
