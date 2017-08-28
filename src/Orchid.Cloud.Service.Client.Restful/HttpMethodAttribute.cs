using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Service.Client.Restful
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class HttpMethodAttribute : Attribute
    {
        public HttpMethod HttpMethod { get; private set; }

        public HttpMethodAttribute(HttpMethod httpMethod = HttpMethod.GET)
        {
            HttpMethod = httpMethod;
        }
    }

    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}
