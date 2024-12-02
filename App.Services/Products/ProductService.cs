using App.Repositories;
using App.Repositories.Products;
using System.Net;

namespace App.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductAsync(count);

        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if(product is  null)
        {
            ServiceResult<Product>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto!);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
        };

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id));
    }

    //Fast Fail

    //Guard Clauses

    public async Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest updateProductRequest)
    {
        var product = await productRepository.GetByIdAsync(id);

        if(product is null)
        {
            return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
        }

        product.Name = updateProductRequest.Name;
        product.Price = updateProductRequest.Price;
        product.Stock = updateProductRequest.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteProductAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Fail($"Product {id} does not exist");
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }
}
