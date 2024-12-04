using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Yapıcı (Constructor)
    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await _productRepository.GetTopPriceProductAsync(count);

        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productRepository.GetAll().ToListAsync();

        var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
    {
        // 1 - 10 first ten record skip(0).take(10)
        // 2 - 10 second ten record skip(10).take(10)
        // 3 - 10 third ten record skip(20).take(10)

        var products = await _productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if(product is  null)
        {
            ServiceResult<Product>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
        };

        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreate(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }

    //Fast Fail

    //Guard Clauses

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if(product is null)
        {
            return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
        }

        product.Name = updateProductRequest.Name;
        product.Price = updateProductRequest.Price;
        product.Stock = updateProductRequest.Stock;

        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return ServiceResult.Fail($"Product {id} does not exist");
        }

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
