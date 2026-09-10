using Application.Contracts;
using Domain.Entities;
using InfraStructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repositories;

public class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public async Task<Customer?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Customers.FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}