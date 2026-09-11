using AutoMapper;
using Domain.Entities;

namespace Application.Features.Orders.Queries.GetOrderById;

public class GetOrderProfile : Profile
{
    public  GetOrderProfile()
    {
        CreateMap<Order, OrderToReturnDto>();
        CreateMap<OrderItem, OrderItemToReturnDto>();
    }
}