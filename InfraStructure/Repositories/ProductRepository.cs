using Application.Contracts;
using Domain.Entities;
using InfraStructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken cancellationToken = default)
    {
        var idList = ids.Distinct().ToList();

        return await context.Products
            .Where(p => idList.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }
}