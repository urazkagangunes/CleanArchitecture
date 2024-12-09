using App.Services.Products;

namespace App.Services.Categories.Dto;

public record CategoryWithProductDto(int Id, string Name, List<ProductDto> ProductDtos);