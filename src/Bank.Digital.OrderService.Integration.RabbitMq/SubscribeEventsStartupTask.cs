using System.Threading;
using System.Threading.Tasks;
using Bank.Common.WebApi.HostedServices;
using Bank.Digital.OrderService.Integration.RabbitMq.Consumers;
using Bank.Digital.OrderService.Integration.RabbitMq.EventHandlers;

namespace Bank.Digital.OrderService.Integration.RabbitMq
{
    internal class SubscribeEventsStartupTask : IStartupTask
    {
        public SubscribeEventsStartupTask(OrderStateChangedConsumer orderStateChangedConsumer)
        {
            orderStateChangedConsumer.Subscribe<OrderStateChangedHandler>();

            //producers shall create exchanges during initialization from di. Do not remove not used parameters
        }

        /// <inheritdoc />
        public Task Execute(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
