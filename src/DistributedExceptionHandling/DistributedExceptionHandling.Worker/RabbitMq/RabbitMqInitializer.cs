using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;
using DistributedExceptionHandling.Worker.Service.Impl;
using Common.Logging;
using Common.DistribudetExcetionHandling;

namespace DistributedExceptionHandling.Worker.RabbitMq
{
    public static class RabbitMqInitializer
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("rabbitmq-connection");
            services.Configure<RabbitMqOptions>(section);

            var sectionQueue = configuration.GetSection("rabbitmq-configuration");
            services.Configure<RabbitQueueOptions>(sectionQueue);

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitMqModelPooledObjectPolicy>(); 
            services.AddHostedService<RabbitMessageHandlerService>();
  
            return services;
        }
    }
}