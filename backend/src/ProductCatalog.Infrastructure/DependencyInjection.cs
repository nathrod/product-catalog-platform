using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Interfaces;
using ProductCatalog.Infrastructure.Cloudinary;
using SqlSugar;

namespace ProductCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            
            services.AddSingleton<ISqlSugarClient>(_ => new SqlSugarScope(new ConnectionConfig
            {
                ConnectionString = connectionString,
                DbType = DbType.PostgreSQL,
                IsAutoCloseConnection = true
            }));

            services.Configure<CloudinarySettings>(
                configuration.GetSection(
                    CloudinarySettings.SectionName
                )
            );

            services.AddScoped<
                IImageStorageService,
                CloudinaryImageStorageService
            >();

            return services;
        }
    }   
}
