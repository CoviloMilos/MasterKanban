using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.ExceptionHandling.Exceptions;
using KanbanGateway.API.Middlewares;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using NLog;
using Microsoft.Extensions.DependencyInjection;
using Common.Logging;
using NLog.Extensions.Logging;

namespace KanbanGateway.API
{
    public class Program
    {
        /*public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((host, config) => { config.AddJsonFile("configuration.json"); })
                .UseStartup<Startup>();*/

        public static void Main(string[] args) {
            new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .ConfigureAppConfiguration((hostingContext, config) => {
                   config
                       .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                       .AddJsonFile("appsettings.json", true, true)
                       .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                       .AddJsonFile("configuration.json")
                       .AddEnvironmentVariables();
               })
               .ConfigureServices(s => {
                   s.AddOcelot();
               })
               .ConfigureLogging((hostingContext, logging) => {

               })
               .UseIISIntegration()
               .Configure((app) => {
                   LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
                   app.ApplicationServices.GetService<ILoggerFactory>().AddNLog();

                   var config = new OcelotPipelineConfiguration {

                       PreErrorResponderMiddleware = async ( context, next) => {

                           try {
                               await next.Invoke();
                           } catch (BaseException ex) {
                               await OcelotExceptionHandling.HandleExceptionAsync(context, ex);
                           }

                           await next.Invoke();
                       }
                   };
                   app.UseOcelot(config).Wait();
               })
               .Build()
               .Run();
        }

    }
}
