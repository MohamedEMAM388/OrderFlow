using Domain.Entities;

namespace Application.Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}