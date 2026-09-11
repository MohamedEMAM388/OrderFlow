using Application.Behaviors;
using Application.Features.Orders.Commands.CreateOrder;
using Application.Features.Orders.Queries.GetOrderById;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<GetOrderProfile>();
        });
        
        return services;
    }
}