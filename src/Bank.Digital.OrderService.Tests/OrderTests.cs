using System;
using Bank.Common.WebApi.Swagger;
using Bank.Digital.OrderService.Api;
using Bank.Digital.OrderService.Api.Client;
using Bank.Digital.OrderService.Api.Contract.Orders;
using Bank.Digital.OrderService.Tests.Mock;
using Xunit;

namespace Bank.Digital.OrderService.Tests
{
    public class OrderTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly OrderServiceClient _client;

        public OrderTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = new OrderServiceClient(factory.CreateClient());
        }

        /// <summary>
        /// Корректно создает и получает заявку, используя АПИ
        /// </summary>
        [Fact]
        public async void CanValidateOrderCreateRequest()
        {
            var ex = await Assert.ThrowsAsync<ApiClientErrorException>(() => _client.CreateOrderAsync(new OrderCreateRequest
            {
                ProductId = null,
                ApplicantId = 1,
                DateAdd = DateTimeOffset.Now
            }));
            Assert.Equal(400, ex.StatusCode);

            ex = await Assert.ThrowsAsync<ApiClientErrorException>(() => _client.CreateOrderAsync(new OrderCreateRequest
            {
                ProductId = 1,
                ApplicantId = null,
                DateAdd = DateTimeOffset.Now
            }));
            Assert.Equal(400, ex.StatusCode);

            ex = await Assert.ThrowsAsync<ApiClientErrorException>(() => _client.CreateOrderAsync(new OrderCreateRequest
            {
                ProductId = 1,
                ApplicantId = 1,
                DateAdd = null
            }));
            Assert.Equal(400, ex.StatusCode);
        }

        [Fact]
        public async void CanCreateOrder()
        {
            const int productId = 1;
            const int applicantId = 2;

            //so complex because PG rounds milliseconds to 6 digits
            var now = DateTimeOffset.Now;
            var dateAdd = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, 123, now.Offset);

            var request = new OrderCreateRequest
            {
                ProductId = productId,
                ApplicantId = applicantId,
                DateAdd = dateAdd
            };

            var order = await _client.CreateOrderAsync(request);

            Assert.Equal(applicantId, order.ApplicantId);
            Assert.Equal(productId, order.ProductId);
            Assert.Equal(dateAdd, order.DateAdd);

            var orderId = order.Id;

            order = await _client.GetOrderAsync(orderId);

            Assert.Equal(orderId, order.Id);
            Assert.Equal(applicantId, order.ApplicantId);
            Assert.Equal(productId, order.ProductId);
            Assert.Equal(dateAdd, order.DateAdd);
        }
    }
}