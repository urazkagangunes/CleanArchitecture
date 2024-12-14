using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;

namespace App.Application.Features.Categories;

public interface ICategoryService
{
    Task<ServiceResult<CategoryWithProductDto>> GetCategoryWithProductAsync(int categoryId);
    Task<ServiceResult<List<CategoryWithProductDto>>> GetCategoryWithProductAsync();
    Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
    Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
    Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest);
    Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest);
    Task<ServiceResult> DeleteAsync(int id);
}
