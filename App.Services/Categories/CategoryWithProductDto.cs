using App.Services.Products;

namespace App.Services.Categories;

public record CategoryWithProductDto(int Id, string Name, List<ProductDto> ProductDtos);