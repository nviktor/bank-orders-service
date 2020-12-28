using AutoMapper;
using Bank.Common.Enums;
using Bank.Digital.OrderService.Domain.Model;

namespace Bank.Digital.OrderService.DAL.Linq2Db.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DAL.Linq2Db.Context.Order, Order>()
                .ForMember(d => d.State, _ => _.MapFrom(s => s.State.ToEnum<OrderState>(true)));
        }
    }
}
