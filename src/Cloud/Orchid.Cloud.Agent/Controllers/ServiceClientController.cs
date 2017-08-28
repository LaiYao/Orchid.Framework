using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orchid.Cloud.Agent.Filters;
using Orchid.Cloud.ServiceRegistry.Abstractions;
using Orchid.Cloud.LoadBalance.Abstractions;
using Orchid.Cloud.Service.Client.Abstractions;

namespace Orchid.Cloud.Agent.Controllers
{
    /// <summary>
    /// Agent的一个主要职责是代理真实服务对外访问其他的服务，这样可以管理对服务的引用过程，以便集成相关的基础服务
    /// </summary>
    [Produces("application/json")]
    [Route("agent/[controller]")]
    [LocalAccessOnly]
    public class ServiceClientController : Controller
    {
        #region | Fields |

        IServiceRegistry _serviceRegistry;
        ILoadBalance _loadBalance;

        #endregion

        [HttpGet]
        public void Invoke([FromBody]IInvocation invocation)
        {

        }
    }
}