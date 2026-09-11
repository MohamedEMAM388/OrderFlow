using Application.Contracts;
using InfraStructure.BackgroundServices;
using InfraStructure.Caching;
using InfraStructure.Persistence.Data;
using InfraStructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace InfraStructure;

public static class DependencyInjection
{ 
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        // redis cache
        services.AddSingleton<IConnectionMultiplexer>(cfg =>
        {
            var connectionString = configuration.GetConnectionString("RedisConnection");
            return ConnectionMultiplexer.Connect(connectionString!);
        });

        services.AddScoped<ICacheRepository, CacheRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // background service
        
        services.AddScoped<IOrderProcessingService, OrderProcessingService>();
        services.AddHostedService<OrderProcessingBackgroundService>();
        
        return services;
    }

}