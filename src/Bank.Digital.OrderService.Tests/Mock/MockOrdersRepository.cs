using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bank.Digital.OrderService.DAL.Abstractions;
using Bank.Digital.OrderService.Domain.Model;

namespace Bank.Digital.OrderService.Tests.Mock
{
    internal class MockOrdersRepository: IOrdersRepository
    {
        /// <inheritdoc />
        public Task<IReadOnlyCollection<Order>> GetApplicantOrders(int applicantId)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<Order> GetOrder(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task CreateOrder(Order order, OrderContext context)
        {
            throw new Exception("Тунцни лососца");
        }

        /// <inheritdoc />
        public Task UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
