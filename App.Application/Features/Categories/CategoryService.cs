using App.Application.Contracts.Persistence;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest createCategoryRequest)
    {
        var anyCategory = await _categoryRepository.AnyAsync(n => n.Name == createCategoryRequest.Name);

        if (anyCategory)
        {
            return ServiceResult<int>.Fail("Category name is already taken.", HttpStatusCode.NotFound);
        }

        var category = _mapper.Map<Category>(createCategoryRequest);

        await _categoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult<int>.SuccessAsCreate(category.Id, $"api/categories/{category.Id}");
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        //if(category is null)
        //{
        //    return ServiceResult.Fail($"Category {id} does not exist");
        //}

        _categoryRepository.Delete(category!);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        var categoriesAsDto = _mapper.Map<List<CategoryDto>>(categories);

        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
        {
            return ServiceResult<CategoryDto?>.Fail($"Category {id} not found!", HttpStatusCode.NotFound)!;
        }

        var categoryAsDto = _mapper.Map<CategoryDto>(category);

        return ServiceResult<CategoryDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<CategoryWithProductDto>> GetCategoryWithProductAsync(int categoryId)
    {
        var category = await _categoryRepository.GetCategoryWithProductAsync(categoryId);

        if (category is null)
        {
            return ServiceResult<CategoryWithProductDto>.Fail("Category not found.", HttpStatusCode.NotFound);
        }

        var categoryAsDto = _mapper.Map<CategoryWithProductDto>(category);

        return ServiceResult<CategoryWithProductDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<List<CategoryWithProductDto>>> GetCategoryWithProductAsync()
    {
        var category = await _categoryRepository.GetCategoryWithProductAsync();

        var categoryAsDto = _mapper.Map<List<CategoryWithProductDto>>(category);

        return ServiceResult<List<CategoryWithProductDto>>.Success(categoryAsDto);
    }

    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest updateCategoryRequest)
    {
        //var category = await _categoryRepository.GetByIdAsync(id);

        //if (category is null)
        //{
        //    return ServiceResult.Fail("Category not found.", HttpStatusCode.NotFound);
        //}

        var isNameTaken = await _categoryRepository.AnyAsync(n => n.Name == updateCategoryRequest.Name && n.Id != id);

        if (isNameTaken)
        {
            return ServiceResult.Fail("Product name is already taken", HttpStatusCode.BadRequest);
        }

        var category = _mapper.Map<Category>(updateCategoryRequest);
        category.Id = id;
        _categoryRepository.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
