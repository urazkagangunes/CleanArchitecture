using App.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class ProductsController(ProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await productService.GetAllAsync());

        [HttpGet]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest) => 
            CreateActionResult(await productService.CreateAsync(createProductRequest));

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateProductRequest updateProductRequest) => 
            CreateActionResult(await productService.UpdateAsync(id, updateProductRequest));

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await productService.DeleteAsync(id));
    }
}
