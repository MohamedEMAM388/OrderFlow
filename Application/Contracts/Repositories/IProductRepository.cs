using Domain.Entities;

namespace Application.Contracts;

public interface IProductRepository
{
    Task<List<Product>> GetByIdsAsync(
        IEnumerable<int> ids,
        CancellationToken cancellationToken = default);
}