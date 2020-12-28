using Microsoft.Extensions.DependencyInjection;

namespace Bank.Digital.OrderService.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services
                .AddTransient<OrdersService>();

            return services;
        }
    }
}
