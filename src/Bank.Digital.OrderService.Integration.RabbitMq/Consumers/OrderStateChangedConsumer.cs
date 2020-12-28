using System;
using Bank.Digital.OrderService.Integration.RabbitMq.Contract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bank.Digital.OrderService.Integration.RabbitMq.Consumers
{
    internal class OrderStateChangedConsumer : RabbitConsumerBase<OrderStateChangedMessage>
    {
        /// <inheritdoc />
        public OrderStateChangedConsumer(IOptions<RabbitMqConfig> configuration,
            ILogger<OrderStateChangedConsumer> logger, IServiceProvider serviceProvider) : base(configuration,
            logger, serviceProvider)
        {
        }

        /// <inheritdoc />
        protected override string ExchangeName => "bank.digital.eventbus";

        /// <inheritdoc />
        protected override string QueueName => "bank.ordersService.newOrderState";

        /// <inheritdoc />
        protected override string RoutingKey => "new-order-state";
    }
}