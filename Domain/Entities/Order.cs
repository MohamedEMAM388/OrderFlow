using Domain.Entities.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Order :BaseEntity<Guid>
{
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;

    public ICollection<OrderItem> Items { get; private set; } = [];
    
    public DateTime OrderDate { get; private set; }

    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;
    public decimal TotalPrice { get; private set; }
    
    private Order() { } 

    public static Order Create(int customerId, List<OrderItem> items)
    {
        
        if (items is null || items.Count == 0)
            throw new DomainException("Order must contain at least one item");

         
        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.ProductName))
                throw new DomainException("Product name is required");

            if (item.Quantity <= 0)
                throw new DomainException("Item quantity must be greater than zero");

            if (item.UnitPrice < 0)
                throw new DomainException("Item unit price cannot be negative");
        }

        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            OrderStatus = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow,
            Items = items,
            TotalPrice = items.Sum(x => x.Quantity * x.UnitPrice)
        };
    }

}

