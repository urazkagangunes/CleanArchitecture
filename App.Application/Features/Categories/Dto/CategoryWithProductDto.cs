using App.Application.Features.Products.Dto;

namespace App.Application.Features.Categories.Dto;

public class CategoryWithProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<ProductDto> ProductDtos { get; set; } = new();
}