namespace Domain.Entities;

public class Customer : BaseEntity<int>
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}