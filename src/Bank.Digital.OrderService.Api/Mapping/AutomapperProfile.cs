using AutoMapper;
using Bank.Common.Enums;
using Bank.Digital.OrderService.Api.Contract.Orders;
using Bank.Digital.OrderService.Domain.DTO;

namespace Bank.Digital.OrderService.Api.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Domain.Model.Order, Order>()
                .ForMember(d => d.State, _ => _.MapFrom(s => s.State.ToString().ToEnum<OrderState>(true)));

            CreateMap<OrderCreateRequest, OrderCreationData>();
        }
    }
}