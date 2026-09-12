namespace Application.Features.Orders.Queries.GetOrders;

public class OrderListItem
{
    public Guid Id { get; set; }
    public int CustomerId { get; set; }
    public string OrderStatus { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
}