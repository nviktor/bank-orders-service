using Bank.Common.WebApi.HostedServices;
using Bank.Digital.OrderService.Integration.RabbitMq.Consumers;
using Bank.Digital.OrderService.Integration.RabbitMq.EventHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Digital.OrderService.Integration.RabbitMq
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqServices(this IServiceCollection services)
        {
            services
                .AddSingleton<OrderStateChangedConsumer>()
                .AddTransient<OrderStateChangedHandler>()
                .AddTransient<IStartupTask, SubscribeEventsStartupTask>();

            return services;
        }
    }
}
