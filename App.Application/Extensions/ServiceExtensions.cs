using App.Application.Features.Categories;
using App.Application.Features.Products;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Application.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiBehaviorOptions>(opt => opt.SuppressModelStateInvalidFilter = true);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        //services.AddScoped(typeof(NotFoundFilter<,>));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //services.AddScoped<CriticalExceptionHandler>();
        //services.AddScoped<GlobalExceptionHandler>();

        return services;
    }
}
