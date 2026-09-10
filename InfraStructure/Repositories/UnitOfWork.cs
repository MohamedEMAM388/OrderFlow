using Application.Contracts;
using InfraStructure.Persistence.Data;

namespace InfraStructure.Repositories;

public class UnitOfWork(AppDbContext context, IOrderRepository orderRepository) 
    : IUnitOfWork
{
    public IOrderRepository OrderRepository { get; } = orderRepository;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}