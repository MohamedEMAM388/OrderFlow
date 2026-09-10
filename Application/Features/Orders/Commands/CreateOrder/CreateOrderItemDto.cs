namespace Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderItemDto(
    int ProductId, 
    string ProductName, 
    int Quantity, decimal 
        UnitPrice);