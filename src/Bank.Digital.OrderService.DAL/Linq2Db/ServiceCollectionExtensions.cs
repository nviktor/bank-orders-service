using Bank.Digital.OrderService.DAL.Abstractions;
using Bank.Digital.OrderService.DAL.Linq2Db.Context;
using Bank.Digital.OrderService.DAL.Linq2Db.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Digital.OrderService.DAL.Linq2Db
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавляет сервисы DAL
        /// </summary>
        public static IServiceCollection AddLinq2DbDalServices(this IServiceCollection services)
        {
            services
                .AddScoped<IOrdersRepository, OrdersRepository>()
                .AddScoped<OrderServiceDbContext>();

            return services;
        }
    }
}
