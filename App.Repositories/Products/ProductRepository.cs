using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Products;

public class ProductRepository(AppDbContext appDbContext) : GenericRepository<Product, int>(appDbContext), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductAsync(int count)
    {
        return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}