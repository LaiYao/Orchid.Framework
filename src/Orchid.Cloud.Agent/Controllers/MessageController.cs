using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orchid.Cloud.Agent.Abstractions;
using Orchid.Cloud.Agent.Filters;

namespace Orchid.Cloud.Agent.Controllers
{
    [Produces("application/json")]
    [Route("agent/Message")]
    [LocalAccessOnly]
    public class MessageController : Controller, IMessageService
    {
    }
}