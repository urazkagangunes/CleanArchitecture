using App.Repositories.Products;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
}
