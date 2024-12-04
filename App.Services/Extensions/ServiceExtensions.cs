using App.Repositories;
using App.Repositories.Products;
using App.Services.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Services.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<ServiceResult>();
        //services.AddScoped<IProductRepository, ProductRepository>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
