using Microsoft.VisualBasic.CompilerServices;
using System;
using System.IO;
using KanbanManagement.API.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using AutoMapper;
using KanbanManagement.API.Repository.Impl;
using KanbanManagement.API.Service;
using KanbanManagement.API.Service.Impl;
using Microsoft.OpenApi.Models;
using Common.ExceptionHanding;
using Common.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using KanbanManagement.API.Shared.HealthCheck;
using Common.DistribudetExcetionHandling;
using KanbanManagement.API.Shared;

namespace KanbanManagement.API
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => 
                {   //Ignore nested json
                    options.SerializerSettings.ReferenceLoopHandling = 
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddRabbitMq(Configuration);

            services.AddDbContext<DataContext>(opt => 
                {
                    opt.UseNpgsql(Configuration.GetConnectionString("postgres-connection"));
                });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KanbanManagement", Version = "v1" });
                });

            // HealthCheck configuration
            services.AddHealthChecks()
                .AddRabbitMQ(KanbanManagement.API.Shared.Utils
                                .extractRabbitMqConfigDataToString(Configuration.GetSection("rabbitmq-connection").Get<RabbitMqOptions>()), 
                             "Kanban management RabbitMq HC")
                .AddCheck("Kanban management Postgresql HC", new NpgsqlHealthCheck(Configuration.GetConnectionString("postgres-connection")));
            services.AddHealthChecksUI();

            // AutoMapper configuration
            Mapper.Reset();
            services.AddAutoMapper();

            // Services configuration
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IAssignmentService, AssignmentService>();

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

            // HealthCheck middleware
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI(delegate (Options options)
            {
                options.UIPath = "/hc-ui";
                options.AddCustomStylesheet("./Shared/HealthCheck/Customization/custom.css");
            });

            // Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KanbanManagement");
                });

            app.UseRequestInvokerMiddleware();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
