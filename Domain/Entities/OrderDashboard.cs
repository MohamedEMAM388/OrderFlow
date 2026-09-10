using Domain.Entities.Enums;

namespace Domain.Entities;

public class OrderDashboard : BaseEntity<int>
{
    public string CustomerName { get; set; } = null!;
    public int ItemCount { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus OrderStatus { get; set; }

}