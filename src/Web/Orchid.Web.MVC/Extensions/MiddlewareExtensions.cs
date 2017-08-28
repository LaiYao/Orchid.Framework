using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Orchid.Web.MVC.Middleware;

namespace Orchid.Web.MVC.Extensions
{
    public static class MiddlewareExtensions
    {
        #region | Logger Middleware |

        public static IApplicationBuilder UseRequestLoggerMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLogMiddleware>();
        }

        #endregion

        #region | ProcessingTimeMiddleware |

        public static IApplicationBuilder UseProcessingTimeMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLogMiddleware>();
        }

        public static IApplicationBuilder UseProcessingTimeMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            return app.UseMiddleware<RequestLogMiddleware>(logger);
        }

        #endregion
    }
}
