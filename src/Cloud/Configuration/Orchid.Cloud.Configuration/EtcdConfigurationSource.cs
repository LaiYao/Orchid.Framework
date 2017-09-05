using EtcdNet;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Configuration
{
    public class EtcdConfigurationSource : IConfigurationSource
    {
        EtcdClientOpitions _clientOptions;

        public EtcdConfigurationSource(string[] urls, string user, string pwd)
        {
            _clientOptions = new EtcdClientOpitions
            {
                Urls = urls,
                Username = user,
                Password = pwd,
                IgnoreCertificateError = true,
                UseProxy = false,
            };
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EtcdConfigurationProvider(_clientOptions);
        }
    }
}
