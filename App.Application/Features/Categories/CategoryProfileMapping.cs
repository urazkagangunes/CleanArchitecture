using App.Application.Features.Categories.Create;
using App.Application.Features.Categories.Dto;
using App.Application.Features.Categories.Update;
using App.Application.Features.Products.Dto;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Categories;

public class CategoryProfileMapping : Profile
{
    public CategoryProfileMapping()
    {
        //CreateMap<CategoryDto, Category>().ReverseMap();

        ////CreateMap<Category, CategoryWithProductDto>()
        ////    .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Products));
        ////CreateMap<Category, CategoryWithProductDto>().ReverseMap();

        //CreateMap<Category, CategoryWithProductDto>()
        //    .ConstructUsing(src => new CategoryWithProductDto(src.Id, src.Name, new List<ProductDto>()));

        //CreateMap<Product, ProductDto>();

        //CreateMap<CreateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        //CreateMap<UpdateCategoryRequest, Category>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<CategoryDto, Category>().ReverseMap();

        // Doğru Mapleme
        CreateMap<Category, CategoryWithProductDto>()
            .ForMember(dest => dest.ProductDtos, opt => opt.MapFrom(src => src.Products));

        CreateMap<Product, ProductDto>();

        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<UpdateCategoryRequest, Category>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}

