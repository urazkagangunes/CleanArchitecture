using App.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;

        // Yapıcı (Constructor)
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _productService.GetAllAsync());

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) => 
            CreateActionResult(await _productService.GetPagedAllAsync(pageNumber, pageSize)); 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest) => 
            CreateActionResult(await _productService.CreateAsync(createProductRequest));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest updateProductRequest) => 
            CreateActionResult(await _productService.UpdateAsync(id, updateProductRequest));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _productService.DeleteAsync(id));
    }
}
