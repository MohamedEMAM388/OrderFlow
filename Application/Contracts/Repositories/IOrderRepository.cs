using Domain.Entities;

namespace Application.Contracts;

public interface IOrderRepository
{
    Task<Guid> CreateAsync(Order order, CancellationToken cancellationToken = default);

    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<OrderDashboard>> GetPendingOrdersAsync(CancellationToken cancellationToken = default);

    void UpdateAsync(Order order);
    
    Task<IEnumerable<OrderDashboard>> GetDashboardAsync(CancellationToken cancellationToken = default);
}