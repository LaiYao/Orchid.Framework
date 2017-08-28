using System;
using Orchid.Cloud.LoadBalance.Abstractions;
using Orchid.Cloud.Agent;

namespace Orchid.Cloud.LoadBalance.Agent
{
    public class AgentLoadBalance : ILoadBalance
    {
        #region | Fields |

        string 

        #endregion

        public AgentLoadBalance()
        {
            var agentPort = Environment.GetEnvironmentVariable(AgentConstants.ENV_PORT_KEY);
        }

        public string SelectActualUri(string target)
        {
            throw new NotImplementedException();
        }
    }
}
