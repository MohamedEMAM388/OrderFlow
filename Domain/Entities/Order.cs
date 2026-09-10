using Domain.Entities.Enums;

namespace Domain.Entities;

public class Order :BaseEntity<Guid>
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public ICollection<OrderItem> Items { get; set; } = [];
    
    public DateTime OrderDate { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending; 
    public decimal TotalPrice { get; set; }
    
}

