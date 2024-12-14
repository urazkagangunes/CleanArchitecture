using App.Application.Features.Categories;
using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Update;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllList() => CreateActionResult(await _categoryService.GetAllListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _categoryService.GetByIdAsync(id));

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id) => CreateActionResult(await _categoryService.GetCategoryWithProductAsync(id));

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts() => CreateActionResult(await _categoryService.GetCategoryWithProductAsync());

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest createCategoryRequest) =>
            CreateActionResult(await _categoryService.CreateAsync(createCategoryRequest));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest) =>
            CreateActionResult(await _categoryService.UpdateAsync(id, updateCategoryRequest));

        [ServiceFilter(typeof(NotFoundFilter<Category, int>))]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await _categoryService.DeleteAsync(id));
    }
}
