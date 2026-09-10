namespace Application.Contracts;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }    
    ICustomerRepository CustomerRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}