using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Digital.OrderService.Migrations.OrderServiceDb
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Add services, required for OrderServiceDb migrations
        /// </summary>
        public static IServiceCollection AddOrderServiceDbMigrations(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddFluentMigratorCore()
                .ConfigureRunner(r =>
                    r.AddPostgres()
                        .WithGlobalConnectionString(config.GetConnectionString("OrderServiceDb"))
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging()
                .BuildServiceProvider(false);

            return services;
        }
    }
}
