using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Options.ConfigurationExtensions;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Orchid.Cloud.Abstractions;
using System.Reflection;

namespace Orchid.Cloud
{
    public class CloudApplication : ICloudApplication
    {
        static readonly List<Type> _typeCache = new List<Type>();

        public IConfigurationRoot Configuration
        {
            get;
            private set;
        }

        public Queue<IMiddleware> Pipeline { get; } = new Queue<IMiddleware>();

        public virtual void Configure(IServiceCollection services)
        {
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //services.Configure<CloudApplicationOptions>("CloudApplication");
        }

        public virtual void Start()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("bootstrap.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            Configure(serviceCollection);

            var runListeners = Configuration.GetSection("CloudApplicationRunListeners");


        }

        public static void Run(Type type)
        {
            var application = new CloudApplication();
            //var attribute=type.

            //application.Start();
        }

        #region | Events |

        // TODO: use events to listen the application events

        public event EventHandler ApplicationStarting;
        public event EventHandler ApplicationStarted;

        public event EventHandler ApplicationContainerServicePreparing;
        public event EventHandler ApplicationContainerServicePrepared;

        #endregion
    }
}
