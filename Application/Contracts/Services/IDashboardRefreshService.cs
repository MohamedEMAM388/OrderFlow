namespace Application.Contracts;

public interface IDashboardRefreshService
{
    Task RefreshDashboardAsync(CancellationToken cancellationToken);
}