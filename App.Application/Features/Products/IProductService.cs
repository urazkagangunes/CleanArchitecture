using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateProductStock;

namespace App.Application.Features.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest updateProductRequest);
    Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest updateProductStockRequest);
    Task<ServiceResult> DeleteAsync(int id);
}