using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<ServiceResult>();
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<CriticalExceptionHandler>();
        services.AddScoped<GlobalExceptionHandler>();

        return services;
    }
}
