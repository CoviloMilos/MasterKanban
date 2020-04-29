using Common.DistribudetExcetionHandling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace Common.Initializers
{
    public static class RabbitMqInitializer
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("rabbitmq-connection");
            services.Configure<RabbitMqOptions>(section);
            
            var sectionExchange = configuration.GetSection("rabbitmq-exchange-config");
            services.Configure<RabbitMqExchangeOptions>(sectionExchange);

            services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
            services.AddSingleton<IPooledObjectPolicy<IModel>, RabbitMqModelPooledObjectPolicy>(); 
            services.AddSingleton<IRabbitMqManager, RabbitMqManager>();
  
            return services;
        }
    }
}