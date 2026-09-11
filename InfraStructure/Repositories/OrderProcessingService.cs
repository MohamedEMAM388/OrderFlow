using Application.Contracts;
using Domain.Entities.Enums;
using InfraStructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories;

public class OrderProcessingService(AppDbContext context , 
    ICacheService cacheService) : IOrderProcessingService
{
    public async Task ProcessPendingOrdersAsync(CancellationToken cancellationToken = default)
    {
        var pendingOrders = await context.Orders
            .Where(x => x.OrderStatus == OrderStatus.Pending).ToListAsync(cancellationToken);

        foreach (var order in pendingOrders)
        {
            order.MarkAsCompleted();
        }
        
        if(pendingOrders.Count > 0)
            await context.SaveChangesAsync(cancellationToken);

        foreach (var order in pendingOrders)
        {
            await cacheService.RemoveDataAsync($"/api/order/{order.Id}".ToLowerInvariant());
        }
    }
}