using Application.Contracts;
using InfraStructure.Persistence.Data;

namespace InfraStructure.Repositories;

public class UnitOfWork(AppDbContext context, IOrderRepository orderRepository , ICustomerRepository customerRepository) 
    : IUnitOfWork
{
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}