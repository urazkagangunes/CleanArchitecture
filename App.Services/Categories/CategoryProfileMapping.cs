using App.Repositories.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products;
using AutoMapper;

namespace App.Services.Categories;

public class CategoryProfileMapping : Profile
{
    public CategoryProfileMapping()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();

        //CreateMap<Category, CategoryWithProductDto>()
        //    .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Products));
        //CreateMap<Category, CategoryWithProductDto>().ReverseMap();

        CreateMap<Category, CategoryWithProductDto>()
            .ConstructUsing(src => new CategoryWithProductDto(src.Id, src.Name, new List<ProductDto>()));

        CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}
