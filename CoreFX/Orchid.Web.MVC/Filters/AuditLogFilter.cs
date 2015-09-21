using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Internal;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Routing;
using Orchid.Core.Utilities;
using System;
using Microsoft.Framework.DependencyInjection;

namespace Orchid.Web.MVC.Filters
{
    public class AuditLogFilter : ActionFilterAttribute
    {
        ILogger _logger = null;

        public string ActionCategory { get; set; }

        public AuditLogFilter([NotNull]IServiceProvider services)
        {
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<AuditLogFilter>();
        }

        public override void OnActionExecuting([NotNull] ActionExecutingContext context)
        {
            var routeInfo = GetRouteInfo(context.RouteData);
            var user = context.HttpContext.User.Identity.Name;

            _logger.LogInformation(string.Format("Action Executting - ActionCategory: {0}; {1} User: {2}", ActionCategory, routeInfo, user));

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted([NotNull] ActionExecutedContext context)
        {
            var routeInfo = GetRouteInfo(context.RouteData);
            var user = context.HttpContext.User.Identity.Name;

            _logger.LogInformation(string.Format("Action Executted - ActionCategory: {0}; {1} User: {2}", ActionCategory, routeInfo, user));

            base.OnActionExecuted(context);
        }

        private string GetRouteInfo(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            return string.Format("Controller: {0}; Action: {1}", controller, action);
        }
    }
}
