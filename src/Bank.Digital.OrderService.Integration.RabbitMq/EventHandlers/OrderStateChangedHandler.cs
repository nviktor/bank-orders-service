using System.Threading.Tasks;
using Bank.Digital.OrderService.Domain;
using Bank.Digital.OrderService.Integration.RabbitMq.Abstractions;
using Bank.Digital.OrderService.Integration.RabbitMq.Contract;

namespace Bank.Digital.OrderService.Integration.RabbitMq.EventHandlers
{
    /// <summary>
    /// Обработчик события "У заказа изменен статус"
    /// </summary>
    internal class OrderStateChangedHandler : IEventHandler<OrderStateChangedMessage>
    {
        private readonly OrdersService _orderService;

        public OrderStateChangedHandler(OrdersService orderService)
        {
            _orderService = orderService;
        }

        /// <inheritdoc />
        public async Task HandleAsync(OrderStateChangedMessage message)
        {
            var newState = (Domain.Model.OrderState?) null;

            switch (message.State)
            {
                case OrderState.ClientHasDboRejected:
                case OrderState.StopfactorsRejected:
                    newState = Domain.Model.OrderState.Rejected;
                    break;

                case OrderState.PushedToIbs:
                    newState = Domain.Model.OrderState.Revision;
                    break;
            }

            if (newState != null)
            {
                var order = await _orderService.GetOrder(message.OrderId);
                order.State = newState.Value;
                await _orderService.UpdateOrder(order);
            }
        }
    }
}