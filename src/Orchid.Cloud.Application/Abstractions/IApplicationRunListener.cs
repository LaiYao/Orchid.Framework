using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Abstractions
{
    public interface IApplicationRunListener
    {
        void Started();
        void EnvironmentPrepared();
        void ContextPrepared();
        void ContextLoaded();
        void Finished();
    }
}
