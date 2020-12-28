using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bank.Digital.OrderService.DAL.Abstractions.Exceptions;
using Bank.Digital.OrderService.Domain.Model;

namespace Bank.Digital.OrderService.DAL.Abstractions
{
    public interface IOrdersRepository
    {
        /// <summary>
        /// Список заявок аппликанта
        /// </summary>
        Task<IReadOnlyCollection<Order>> GetApplicantOrders(int applicantId);

        /// <summary>
        /// Заявка
        /// </summary>
        /// <exception cref="EntityNotFoundException" />
        Task<Order> GetOrder(Guid id);

        /// <summary>
        /// Создать новую заявку
        /// </summary>
        Task CreateOrder(Order order, OrderContext context);

        /// <summary>
        /// Изменить заявку
        /// </summary>
        /// <exception cref="EntityNotFoundException" />
        Task UpdateOrder(Order order);
    }
}
