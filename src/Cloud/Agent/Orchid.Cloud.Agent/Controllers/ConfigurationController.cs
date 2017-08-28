using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orchid.Cloud.Agent.Abstractions;
using Orchid.Cloud.Agent.Filters;

namespace Orchid.Cloud.Agent.Controllers
{
    /// <summary>
    /// Agent的一个重要职责是获取和维护真实服务的配置，以便透明化
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("agent/[controller]")]
    [LocalAccessOnly]
    public class ConfigurationController : Controller, IConfigurationService
    {
        public const string CFG_SVC_ROOT_KEY = "";

        public ConfigurationController()
        {
        }

        // GET api/configuration
        [HttpGet]
        public string Get()
        {
            return string.Empty;
        }

        [HttpGet]
        public string Get(string key)
        {
            throw new NotImplementedException();
        }
    }
}
