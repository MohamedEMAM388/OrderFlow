namespace Application.Contracts;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }    
    ICustomerRepository CustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}