using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Persistence.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Order>  Orders { get; set; }
    public DbSet<Product>  Products { get; set; }
    public DbSet<OrderItem>  OrderItems { get; set; }
    public DbSet<OrderDashboard>   OrderDashboards { get; set; }
    public DbSet<Customer>   Customers { get; set; }
}