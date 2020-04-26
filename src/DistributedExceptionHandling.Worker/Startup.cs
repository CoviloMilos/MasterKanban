using System;
using System.IO;
using AutoMapper;
using Common.Logging;
using DistributedExceptionHandling.Worker.RabbitMq;
using DistributedExceptionHandling.Worker.Repository;
using DistributedExceptionHandling.Worker.Repository.Impl;
using DistributedExceptionHandling.Worker.Service;
using DistributedExceptionHandling.Worker.Service.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace DistributedExceptionHandling.Worker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseNpgsql(Configuration.GetConnectionString("postgres-connection"));
            });

            services.AddRabbitMq(Configuration);

            // Services configuration
            services.AddScoped<IExceptionRepository, ExceptionRepository>();
            services.AddScoped<IExceptionService, ExceptionService>();

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
