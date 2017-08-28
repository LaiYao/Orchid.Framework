using System;

namespace Orchid.Cloud.Service
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ServiceVersionAttribute : Attribute
    {
        const string __DEFAULT_VERSION__ = "1.0";

        public string Version { get; private set; }

        public ServiceVersionAttribute(string version = "")
        {
            Version = string.IsNullOrEmpty(version) ? __DEFAULT_VERSION__ : version;
        }
    }
}
