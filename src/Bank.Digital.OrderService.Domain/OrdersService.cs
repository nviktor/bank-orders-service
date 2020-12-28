using System;
using System.Threading.Tasks;
using Bank.Digital.OrderService.DAL.Abstractions;
using Bank.Digital.OrderService.DAL.Abstractions.Exceptions;
using Bank.Digital.OrderService.Domain.DTO;
using Bank.Digital.OrderService.Domain.Model;

namespace Bank.Digital.OrderService.Domain
{
    /// <summary>
    /// Логика работы с заявками
    /// </summary>
    public class OrdersService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersService(
            IOrdersRepository ordersRepository
            )
        {
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// Создание заявки 
        /// </summary>
        public async Task<Order> CreateOrder(OrderCreationData data)
        {
            var orderId = Guid.NewGuid();

            var order = new Order
            {
                Id = orderId,
                ProductId = data.ProductId,
                ApplicantId = data.ApplicantId,
                State = OrderState.New,
                DateAdd = data.DateAdd
            };

            var context = new OrderContext
            {
                Ip = data.Ip,
                OrderId = orderId,
                Utm = data.Utm,
                GoogleAnalyticsId = data.GoogleAnalyticsId,
                Region = data.Region,
                Browser = data.Browser,
                BrowserUserAgent = data.BrowserUserAgent
            };

            await _ordersRepository.CreateOrder(order, context);

            return order;
        }

        /// <summary>
        /// Обновление заявки 
        /// </summary>
        /// <exception cref="EntityNotFoundException" />
        public async Task UpdateOrder(Order order)
        {
            await _ordersRepository.UpdateOrder(order);
        }

        /// <summary>
        /// Получить заявку по ID
        /// </summary>
        public Task<Order> GetOrder(Guid id)
        {
            return _ordersRepository.GetOrder(id);
        }
    }
}