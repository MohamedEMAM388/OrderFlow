using Application.Contracts;
using Domain.Entities;
using Domain.Entities.Enums;
using InfraStructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories;

public class OrderRepository(AppDbContext context) : IOrderRepository
{
    public async Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        return order.Id;
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Orders.Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);   
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Orders.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<OrderDashboard>> GetPendingOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await context.OrderDashboards
            .Where(x => x.OrderStatus == OrderStatus.Pending)
            .ToListAsync(cancellationToken);
    }

    public  void UpdateAsync(Order order)
    {
         context.Orders.Update(order);
    
    }

    public async Task<IEnumerable<OrderDashboard>> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        return await context.OrderDashboards.AsNoTracking().ToListAsync(cancellationToken);
    }
}