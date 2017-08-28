using Orchid.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Orchid.Cloud.Configuration;

namespace Microsoft.Extensions.Configuration
{
    public static class EtcdConfigurationExtensions
    {
        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, string[] urls, string user, string pwd)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotNull(urls, nameof(urls));

            builder.Add(new EtcdConfigurationSource(urls, user, pwd));

            return builder;
        }
    }
}