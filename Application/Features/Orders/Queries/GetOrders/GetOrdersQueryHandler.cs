using Application.Contracts;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IUnitOfWork unitOfWork 
    , IMapper mapper) : IRequestHandler<GetOrdersQuery , IEnumerable<OrderListItem>>
{
    public async Task<IEnumerable<OrderListItem>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await unitOfWork
                    .OrderRepository.GetAllAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderListItem>>(orders);
    }
}