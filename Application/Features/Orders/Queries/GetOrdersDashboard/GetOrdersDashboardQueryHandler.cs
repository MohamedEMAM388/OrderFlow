using Application.Contracts;
using AutoMapper;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersDashboard;

public class GetOrdersDashboardQueryHandler(IUnitOfWork unitOfWork , IMapper mapper) : 
    IRequestHandler<GetOrdersDashboardQuery , IEnumerable<OrderDashboardDto>>
{
    public async Task<IEnumerable<OrderDashboardDto>> Handle(GetOrdersDashboardQuery request, CancellationToken cancellationToken)
    {
        var ordersDashboard = await unitOfWork.OrderRepository
            .GetDashboardAsync(cancellationToken);

        return mapper.Map<IEnumerable<OrderDashboardDto>>(ordersDashboard);
    }
}