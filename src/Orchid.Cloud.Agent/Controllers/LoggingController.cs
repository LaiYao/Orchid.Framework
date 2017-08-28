using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orchid.Cloud.Agent.Abstractions;
using Orchid.Core.Utilities;
using Orchid.Cloud.Logging;
using Orchid.Cloud.Agent.Filters;

namespace Orchid.Cloud.Agent.Controllers
{
    [Produces("application/json")]
    [Route("agent/[controller]")]
    [LocalAccessOnly]
    public class LoggingController : Controller, ILoggingService
    {
        #region | Fields |

        ILogger _logger;

        #endregion

        public LoggingController()
        {
            var kafkaUrls = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_LOGGING_URLS).Split(',').ToArray();
            var serviceName = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_SERVICE_NAME);
            var kafkaProvider = new KafkaLoggerProvider(new KafkaLoggerOptions { BrokerUrls = kafkaUrls }, LogLevel.Information);
            _logger = kafkaProvider.CreateLogger(serviceName);
        }

        [HttpPost]
        public void Post([FromBody]LoggingEntry entity)
        {
            _logger.Log((LogLevel)entity.LoggingLevel, int.Parse(entity.EventID), entity.Message, null, (_, __) => _);

            //var message = entity.Message;
            //switch (entity.LoggingLevel)
            //{
            //    case LoggingLevel.Trace:
            //        _logger.LogTrace(message);
            //        break;
            //    case LoggingLevel.Debug:
            //        _logger.LogDebug(message);
            //        break;
            //    case LoggingLevel.Information:
            //        _logger.LogInformation(message);
            //        break;
            //    case LoggingLevel.Warning:
            //        _logger.LogWarning(message);
            //        break;
            //    case LoggingLevel.Error:
            //        _logger.LogError(message);
            //        break;
            //    case LoggingLevel.Critical:
            //        _logger.LogCritical(message);
            //        break;
            //    case LoggingLevel.None:
            //        break;
            //    default:
            //        break;
            //}
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                //_logger?.
            }
        }
    }
}
