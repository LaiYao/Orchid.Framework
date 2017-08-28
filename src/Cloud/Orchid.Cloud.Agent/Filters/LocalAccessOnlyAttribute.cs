using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Orchid.Cloud.Agent.Filters
{
    /// <summary>
    /// 作为各内置服务代理角色的服务而言，仅限本地访问
    /// </summary>
    public class LocalAccessOnlyAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.HttpContext.Request.Host.Host != "localhost"
                &&
                context.HttpContext.Request.Host.Host != "127.0.0.1")
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
