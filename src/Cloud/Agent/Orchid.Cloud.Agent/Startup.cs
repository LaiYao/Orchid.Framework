using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orchid.Cloud.Agent.Abstractions;
using Orchid.Cloud.Logging;
using Orchid.Cloud.ServiceRegistry.Abstractions;
using System.Runtime.Loader;

namespace Orchid.Cloud.Agent
{
    public class Startup
    {
        #region | Fields |

        string _configKeyPrefixForAgent;
        string _configKeyPrefixForService;
        string _serviceName;
        int _servicePort;

        IServiceRegistry _serviceRegistry;

        #endregion

        public Startup(IHostingEnvironment env)
        {
            // from builtin env variables
            _serviceName = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_SERVICE_NAME);
            var configUrls = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_CONFIG_URLS).Split(',').ToArray();
            var configUser = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_CONFIG_USER);
            var configPWD = Environment.GetEnvironmentVariable(ApplicationConsts.ENV_CONFIG_PWD);

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .AddEtcd(configUrls, configUser, configPWD);

            Configuration = builder.Build();

            AssemblyLoadContext.Default.Unloading += App_Unloading;
        }

        private void App_Unloading(AssemblyLoadContext obj)
        {
            //_serviceRegistry.Unregister<>($":{_servicePort}", _serviceName);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }

            app.UseMvc();
        }

        #region | Configure Services |

        // TODO: 通过配置的服务注册provider来获取相应的服务注册实例
        void ConfigureServiceRegistry(IServiceCollection services)
        {
            var allProviders = Assembly
                 .GetEntryAssembly()
                 .ExportedTypes
                 .Where(_ => _.IsAssignableFrom(typeof(IServiceRegistryProvider)));

            var providerName = Configuration[$"{_configKeyPrefixForAgent}/service.register.provider/name"];
            Assembly.GetEntryAssembly().GetReferencedAssemblies();

            foreach (var provider in allProviders)
            {
                //services.AddSingleton<IRegistryProvider>()
                services.AddSingleton<IServiceRegistryProvider>((IServiceRegistryProvider)Assembly.GetEntryAssembly().CreateInstance(provider.FullName));
            }
        }

        void ConfigureDynamicConfiguration(IServiceCollection services)
        {

        }

        #endregion

        #region | Initalization |

        private void Initial(IHostingEnvironment env)
        {
            _configKeyPrefixForAgent = $"{ApplicationConsts.CFG_SERVICE_ROOT_KEY}/{_serviceName}/{env.EnvironmentName}/agent";
            _configKeyPrefixForService = $"{ApplicationConsts.CFG_SERVICE_ROOT_KEY}/{_serviceName}/{env.EnvironmentName}/service";

            // resolve ServiceRegistry

            // resolve LoadBalance

            // 
        }

        private void ResolveServiceRegistry()
        {

        }

        private void ResolveLoadBalance()
        {

        }

        private void ResolveServiceConfig()
        {

        }

        #endregion
    }
}
