using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
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

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest) =>
            CreateActionResult(await _categoryService.UpdateAsync(id, updateCategoryRequest));

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id) => CreateActionResult(await _categoryService.DeleteAsync(id));
    }
}
