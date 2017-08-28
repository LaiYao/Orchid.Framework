using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Orchid.Core.Utilities;

namespace Orchid.Web.MVC.Middleware
{
    public class RequestLogMiddleware
    {
        #region | Fields |

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        #endregion

        public RequestLogMiddleware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            Check.NotNull(next, nameof(next));
            Check.NotNull(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogMiddleware>();
        }

        public async Task Invock(HttpContext context)
        {
            _logger.LogInformation("");

            await _next.Invoke(context);

            _logger.LogInformation("");
        }
    }
}
