using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;

namespace App.Services.Categories;

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
