namespace Domain.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public decimal Price { get; set; }
}