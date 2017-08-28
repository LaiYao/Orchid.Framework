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
    /// Agent��һ����Ҫְ���Ǵ�����ʵ���������������ķ����������Թ���Է�������ù��̣��Ա㼯����صĻ�������
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