using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Service.Client.Restful
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RouteAttribute : Attribute
    {
        public string Template { get; private set; }

        public RouteAttribute(string template)
        {
            Template = template;
        }
    }
}
