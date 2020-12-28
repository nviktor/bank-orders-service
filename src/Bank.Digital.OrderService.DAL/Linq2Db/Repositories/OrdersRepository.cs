using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinqToDB;
using Bank.Digital.OrderService.DAL.Abstractions;
using Bank.Digital.OrderService.DAL.Abstractions.Exceptions;
using Bank.Digital.OrderService.DAL.Linq2Db.Context;
using Order = Bank.Digital.OrderService.Domain.Model.Order;

namespace Bank.Digital.OrderService.DAL.Linq2Db.Repositories
{
    internal class OrdersRepository : IOrdersRepository
    {
        private readonly OrderServiceDbContext _db;
        private readonly IMapper _mapper;

        public OrdersRepository(
            OrderServiceDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Order>> GetApplicantOrders(int applicantId)
        {
            var orders = await _db.Orders
                .Where(o => o.ApplicantId == applicantId)
                .ToArrayAsync();

            return orders.Select(_mapper.Map<Order>).ToArray();
        }

        /// <inheritdoc />
        public async Task<Order> GetOrder(Guid id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                throw new EntityNotFoundException();
            }

            return _mapper.Map<Order>(order);

        }

        /// <inheritdoc />
        public async Task CreateOrder(Order order, Domain.Model.OrderContext context)
        {
            using (var transaction = await _db.BeginTransactionAsync())
            {
                await _db.Orders.InsertAsync(() => new Context.Order
                {
                    Id = order.Id,
                    ApplicantId = order.ApplicantId,
                    State = order.State.ToString(),
                    DateAdd = order.DateAdd,
                    ProductId = order.ProductId,
                });

                await _db.OrderContexts.InsertAsync(() => new OrderContext
                {
                    OrderId = order.Id,
                    Ip = context.Ip,
                    Utm = context.Utm,
                    GoogleAnalyticsId = context.GoogleAnalyticsId,
                    Region = context.Region,
                    Browser = context.Browser,
                    BrowserUserAgent = context.BrowserUserAgent
                });

                await transaction.CommitAsync();
            }
        }

        /// <inheritdoc />
        public async Task UpdateOrder(Order order)
        {
            var updated = await _db.Orders.UpdateAsync(o => o.Id == order.Id, o => new Context.Order
            {
                State = order.State.ToString()
            });

            if (updated <= 0)
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
