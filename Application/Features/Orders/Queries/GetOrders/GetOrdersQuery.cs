using MediatR;

namespace Application.Features.Orders.Queries.GetOrders;

public sealed record GetOrdersQuery : IRequest<IEnumerable<OrderListItem>>;

