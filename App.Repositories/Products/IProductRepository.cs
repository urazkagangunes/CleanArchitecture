namespace App.Repositories.Products;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<List<Product>> GetTopPriceProductAsync(int count);
}
