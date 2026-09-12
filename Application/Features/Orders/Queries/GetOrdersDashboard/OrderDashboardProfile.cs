using AutoMapper;
using Domain.Entities;

namespace Application.Features.Orders.Queries.GetOrdersDashboard;

public class OrderDashboardProfile : Profile
{
    public  OrderDashboardProfile()
    {
        CreateMap<OrderDashboard, OrderDashboardDto>()
            .ForMember(dst => dst.OrderStatus, opt =>
                opt.MapFrom(src => src.OrderStatus.ToString()));
    }
}