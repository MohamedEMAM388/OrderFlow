using Application.Contracts;
using Domain.Entities;
using Domain.Entities.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork)
             : IRequestHandler<CreateOrderCommand ,Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
       // items not null
       if (request.Items.Count == 0)
           throw new InvalidOperationException("Order must contain at least one item");

       var items = request.Items
           .Select(x => new OrderItem()
            {
             ProductId = x.ProductId,
             ProductName = x.ProductName,
             Quantity = x.Quantity,
             UnitPrice = x.UnitPrice
           }).ToList();
       var order = Order.Create(request.CustomerId, items);

       await unitOfWork.OrderRepository.CreateAsync(order, cancellationToken);
       await unitOfWork.SaveChangesAsync(cancellationToken);
       return order.Id;
    }
}