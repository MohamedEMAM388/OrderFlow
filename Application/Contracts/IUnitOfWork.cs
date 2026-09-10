namespace Application.Contracts;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}