using Application.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IUnitOfWork unitOfWork)
             : IRequestHandler<CreateOrderCommand ,Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var customer = await unitOfWork.CustomerRepository
            .GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            throw new NotFoundException($"Customer with id {request.CustomerId} was not found.");
        }

        var requestedProductIds = request.Items
            .Select(x => x.ProductId).Distinct().ToList();
        var existingProducts = await unitOfWork.ProductRepository
            .GetByIdsAsync(requestedProductIds, cancellationToken);

        var missingProductIds = requestedProductIds
            .Except(existingProducts.Select(p => p.Id))
            .ToList();

        if (missingProductIds.Count != 0)
        {
            throw new NotFoundException(
                $"Product(s) with id {string.Join(", ", missingProductIds)} were not found.");
        }

        var items = request.Items
            .Select(x => new OrderItem
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