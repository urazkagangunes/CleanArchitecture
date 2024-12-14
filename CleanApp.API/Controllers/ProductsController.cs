using App.Application.Features.Products;
using App.Application.Features.Products.Create;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateProductStock;
using App.Domain.Entities;
using CleanApp.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.API.Controllers
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

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize) =>
            CreateActionResult(await _productService.GetPagedAllAsync(pageNumber, pageSize));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _productService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest createProductRequest) =>
            CreateActionResult(await _productService.CreateAsync(createProductRequest));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest updateProductRequest) =>
            CreateActionResult(await _productService.UpdateAsync(id, updateProductRequest));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest updateProductStockRequest) =>
            CreateActionResult(await _productService.UpdateStockAsync(updateProductStockRequest));

        [ServiceFilter(typeof(NotFoundFilter<Product, int>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _productService.DeleteAsync(id));
    }
}
