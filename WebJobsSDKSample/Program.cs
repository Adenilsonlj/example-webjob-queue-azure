using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Services.ProcessQueueExample;
using Microsoft.Azure.WebJobs.Host;
using Infra.ProcessQueueExample;

namespace WebJobs
{ 
    class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder();

            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorage(a =>
                {
                    a.MaxPollingInterval = TimeSpan.FromSeconds(15);
                });
                b.AddExecutionContextBinding();
            });

            builder.ConfigureAppConfiguration(b =>
             {
                 b.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();
             });

            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
                string instrumentationKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                if (!string.IsNullOrEmpty(instrumentationKey))
                {
                    b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = instrumentationKey);
                }
            });

            builder.ConfigureServices((context, services) =>
            {
                ConfigureServices(context, services);
            });

            var host = builder.Build();

            using (host)
            {
                await host.RunAsync();
            }
        }

        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton(context.Configuration);
            services.AddTransient<IProcessQueueExampleRepository, ProcessQueueExampleRepository>();
            services.AddTransient<IProcessQueueExampleService, ProcessQueueExampleService>();
            services.AddTransient<ProcessQueueExampleFunction>();
        }
    }
}
