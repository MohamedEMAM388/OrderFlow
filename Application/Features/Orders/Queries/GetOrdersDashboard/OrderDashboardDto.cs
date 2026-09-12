namespace Application.Features.Orders.Queries.GetOrdersDashboard;

public class OrderDashboardDto
{
    public string CustomerName { get; set; } = string.Empty;
    public int ItemCount { get; set; }    
    public decimal TotalPrice { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
}