using AutoMapper;
using Domain.Entities;

namespace Application.Features.Orders.Queries.GetOrders;

public class GetOrdersProfile : Profile
{
    public GetOrdersProfile()
    {
        CreateMap<Order, OrderListItem>()
            .ForMember(dst => dst.OrderStatus, opt =>
                opt.MapFrom(src => src.OrderStatus.ToString()));
    }
}