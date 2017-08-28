using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Agent.Abstractions
{
    public interface IConfigurationService
    {
        string Get();

        string Get(string key);
    }
}
