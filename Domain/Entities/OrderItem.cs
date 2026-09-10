namespace Domain.Entities;

public class OrderItem : BaseEntity<int>
{
    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }
}
