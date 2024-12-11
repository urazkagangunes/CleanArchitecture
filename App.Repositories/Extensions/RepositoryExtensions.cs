using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using App.Repositories.Products;
using App.Repositories.Categories;
using App.Repositories.Interceptors;

namespace App.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionStrings = 
            configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

            options.UseSqlServer(connectionStrings!.SqlConnection, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);    
            });
            options.AddInterceptors(new AuditDbContextInterceptor());
        });

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
