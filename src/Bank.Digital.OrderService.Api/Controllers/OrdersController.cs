using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Bank.Digital.OrderService.Api.Contract.Orders;
using Bank.Digital.OrderService.DAL.Abstractions;
using Bank.Digital.OrderService.Domain;
using Bank.Digital.OrderService.Domain.DTO;
using Bank.Digital.OrderService.Domain.Model;
using Microsoft.AspNetCore.Http;
using Order = Bank.Digital.OrderService.Api.Contract.Orders.Order;

namespace Bank.Digital.OrderService.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersService _ordersService;
        private readonly IMapper _mapper;
        private readonly IOrdersRepository _ordersRepository;

        public OrdersController(
            OrdersService ordersService,
            IMapper mapper,
            IOrdersRepository ordersRepository)
        {
            _ordersService = ordersService;
            _mapper = mapper;
            _ordersRepository = ordersRepository;
        }

        /// <summary>
        /// получаем все созданные заявки данным пользователем 
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderList(int applicantId)
        {
            var response = await _ordersRepository.GetApplicantOrders(applicantId);
            return Ok(response.Select(_mapper.Map<Order>));
        }

        /// <summary>
        /// получаем полную информацию по конкретной заявке 
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Order> GetOrder(Guid id)
        {
            var order = await _ordersRepository.GetOrder(id);

            return _mapper.Map<Order>(order);
        }

        /// <summary>
        /// создание новой заявки
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderCreateRequest request)
        {
            var response = await _ordersService.CreateOrder(_mapper.Map<OrderCreationData>(request));

            return _mapper.Map<Order>(response);
        }
    }
}