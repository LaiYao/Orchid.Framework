﻿using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Internal;
using Microsoft.Framework.Logging;
using Microsoft.AspNet.Routing;
using Orchid.Core.Utilities;
using System;
using Microsoft.Framework.DependencyInjection;
using Microsoft.AspNet.Mvc.Filters;

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
            //var request = context.HttpContext.Request;
            //request.HttpContext.User.
            //// Generate an audit  
            //var audit = new
            //{
            //    AuditID = Guid.NewGuid(),
            //    UserName = (request.IsAuthenticated) ? context.HttpContext.User.Identity.Name : "Anonymous",
            //    IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ? ? request.UserHostAddress,
            //    AreaAccessed = request.RawUrl,
            //    TimeAccessed = DateTime.UtcNow
            //};

            //_logger.LogInformation(string.Format("Action Executting - ActionCategory: {0}; {1} User: {2}", ActionCategory, routeInfo, user));

            base.OnActionExecuting(context);
        }
    }
}
