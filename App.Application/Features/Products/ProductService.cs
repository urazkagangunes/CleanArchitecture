using App.Application;
using App.Application.Contracts.Caching;
using App.Application.Contracts.Persistence;
using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateProductStock;
using App.Domain.Entities;
using AutoMapper;
using FluentValidation;
using System.Net;

namespace App.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateProductRequest> _createProductRequestValidator;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    // Yapıcı (Constructor)
    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, 
        IValidator<CreateProductRequest> validator, IMapper mapper, ICacheService cacheService)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _createProductRequestValidator = validator;
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
    }

    private const string ProductListCacheKey = "ProductListCacheKey";
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await _productRepository.GetTopPriceProductAsync(count);

        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);

        return new ServiceResult<List<ProductDto>>()
        {
            Data = productsAsDto
        };
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        //cache aside design pattern
        //1.any cache
        //2.from db
        //3.caching data

        var prodcutListAsCached = await _cacheService.GetAsync<List<ProductDto>>(ProductListCacheKey);

        if(prodcutListAsCached is not null) return ServiceResult<List<ProductDto>>.Success(prodcutListAsCached);

        var products = await _productRepository.GetAllAsync();

        //var productsAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);

        await _cacheService.AddAsync(ProductListCacheKey, productsAsDto, TimeSpan.FromMinutes(1));

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
    {
        // 1 - 10 first ten record skip(0).take(10)
        // 2 - 10 second ten record skip(10).take(10)
        // 3 - 10 third ten record skip(20).take(10)

        var products = await _productRepository.GetAllPagedAsync(pageNumber, pageSize);

        //var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        var productsAsDto = _mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productsAsDto);
    }

    public async Task<ServiceResult<ProductDto?>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if(product is  null)
        {
            return ServiceResult<ProductDto?>.Fail("Product not found", HttpStatusCode.NotFound);
        }

        //var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        var productAsDto = _mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto>.Success(productAsDto)!;
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var anyProduct = await _productRepository.AnyAsync(c => c.Name == request.Name);

        if (anyProduct)
        {
            return ServiceResult<CreateProductResponse>.Fail("Product name is already taken.");
        }

        var product = _mapper.Map<Product>(request); 
        //    new Product()
        //{
        //    Name = request.Name,
        //    Price = request.Price,
        //    Stock = request.Stock,
        //};

        await _productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreate(new CreateProductResponse(product.Id), $"api/products/{product.Id}");
    }

    //Fast Fail

    //Guard Clauses

    public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest)
    {
        //var product = await _productRepository.GetByIdAsync(id);

        //if (product is null)
        //{
        //    return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
        //}

        var isNameTaken = await _productRepository
            .AnyAsync(c => c.Name == updateProductRequest.Name && c.Id != id);

        if (isNameTaken)
        {
            return ServiceResult.Fail("Product name is already taken.", HttpStatusCode.BadRequest);
        }

        //product.Name = updateProductRequest.Name;
        //product.Price = updateProductRequest.Price;
        //product.Stock = updateProductRequest.Stock;

        var product = _mapper.Map<Product>(updateProductRequest);
        product.Id = id;
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest)
    {
        var product = await _productRepository.GetByIdAsync(updateProductStockRequest.ProductId);

        //if(product is null)
        //{
        //    return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
        //}

        product!.Stock = updateProductStockRequest.Quantity;
        
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        //if (product is null)
        //{
        //    return ServiceResult.Fail($"Product {id} does not exist");
        //}

        _productRepository.Delete(product!);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
