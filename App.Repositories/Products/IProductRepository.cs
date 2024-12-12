namespace App.Repositories.Products;

public interface IProductRepository : IGenericRepository<Product, int>
{
    public Task<List<Product>> GetTopPriceProductAsync(int count);
}
