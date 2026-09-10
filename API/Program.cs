using API.Exceptions;
using Application;
using InfraStructure;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Infrastructure
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        

        // Controllers
        builder.Services.AddControllers();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Authentication & Authorization
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        // Swagger
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}