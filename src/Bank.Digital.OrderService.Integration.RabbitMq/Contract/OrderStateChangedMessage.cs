using System;

namespace Bank.Digital.OrderService.Integration.RabbitMq.Contract
{
    internal class OrderStateChangedMessage
    {
        /// <summary>
        /// Id заявки
        /// </summary>
        public Guid OrderId { get; set; }

        /// <summary>
        /// Новое состояние заявки
        /// </summary>
        public OrderState State { get; set; }
    }
}
