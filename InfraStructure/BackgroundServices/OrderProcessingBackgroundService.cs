using Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InfraStructure.BackgroundServices;

public class OrderProcessingBackgroundService(
    IServiceScopeFactory scopeFactory 
    , IConfiguration configuration 
    , ILogger<OrderProcessingBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Order Processing Service started");
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(
                configuration.GetValue("BackgroundJobs:OrderProcessingSeconds", 30)));

        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                using  var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider
                    .GetRequiredService<IOrderProcessingService>();
                
                await service.ProcessPendingOrdersAsync(stoppingToken);
                logger.LogInformation("Order Processing Service completed");

            }
            
        }
        catch (OperationCanceledException ex)
        {
            logger.LogError(ex, "Order Processing Service cancelled");
        }
        
    }
}