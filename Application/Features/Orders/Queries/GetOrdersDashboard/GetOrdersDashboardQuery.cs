using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Queries.GetOrdersDashboard;

public sealed record GetOrdersDashboardQuery : IRequest<IEnumerable<OrderDashboardDto>>;
