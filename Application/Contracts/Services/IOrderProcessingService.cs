using Domain.Entities;

namespace Application.Contracts;

public interface IOrderProcessingService
{
    Task ProcessPendingOrdersAsync(CancellationToken cancellationToken = default);
}