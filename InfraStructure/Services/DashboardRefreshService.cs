using Application.Contracts;
using Domain.Entities;
using InfraStructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace InfraStructure.Services;

public class DashboardRefreshService(AppDbContext context) : IDashboardRefreshService
{
    public async Task RefreshDashboardAsync(CancellationToken cancellationToken)
    {
        var newData = await context.Orders
            .Include(x => x.Items)
            .Include(x => x.Customer)
            .Select(x => new OrderDashboard()
            {
                CustomerName =  x.Customer.Name,
                ItemCount =  x.Items.Count,
                TotalPrice = x.TotalPrice,
                OrderStatus =  x.OrderStatus,
            })
            .ToListAsync(cancellationToken);
        
         // remove old data
         context.OrderDashboards.RemoveRange(context.OrderDashboards);
         
         // add new data 
         await context.AddRangeAsync(newData , cancellationToken);
         await context.SaveChangesAsync(cancellationToken);
        
        
    }
}