using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Service.Client.Restful
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class BodyAttribute : Attribute
    {
    }
}
