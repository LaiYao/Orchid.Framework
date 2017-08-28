using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Orchid.Core.Utilities;
using Orchid.Web.MVC.Constants;

namespace Orchid.Web.MVC.Middleware
{
    public class ProcessingTimeMiddleware
    {
        #region | Fields |

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        #endregion

        public ProcessingTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            Check.NotNull(next, nameof(next));
            Check.NotNull(loggerFactory, nameof(loggerFactory));

            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogMiddleware>();
        }

        public async Task Invock(HttpContext context)
        {
            if (context.Request.Headers.Keys.Contains(HttpHeaderConstants.RequestTracingId))
            {
                var watch = new Stopwatch();
                watch.Start();

                await _next.Invoke(context);

                watch.Stop();
                //context.TraceIdentifier
                _logger.LogInformation($"X-Tracing-Id:{context.Request.Headers[HttpHeaderConstants.RequestTracingId]} {watch.ElapsedMilliseconds} ms");
            }

            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
