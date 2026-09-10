using Application.Contracts;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

}