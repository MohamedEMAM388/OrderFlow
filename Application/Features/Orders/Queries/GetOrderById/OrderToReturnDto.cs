namespace Application.Features.Orders.Queries.GetOrderById;

public class OrderToReturnDto
{
    public Guid Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public ICollection<OrderItemToReturnDto> Items { get; set; } = [];
}