
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Common.Initializers
{
    public static class SwaggerInitializer
    {
        public static IServiceCollection AddSwaggerUI(this IServiceCollection services, IConfiguration configuration)
        {
            // Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(configuration["Service:Version"], new OpenApiInfo
                {
                    Title = configuration["Service:Title"],
                    Version = configuration["Service:Version"],
                    Description = configuration["Service:Description"],
                    Contact = new OpenApiContact
                    {
                        Name = configuration["Service:Contact:Name"],
                        Email = configuration["Service:Contact:Email"]
                    }
                });
            });

            return services;
        }
    }
}