using Domain.Entities;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    int CustomerId
    ,List<CreateOrderItemDto> Items) : IRequest<Guid>;
