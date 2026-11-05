using AutoMapper;
using WebApplicationApi.Models;
using WebApplicationApi.Models.Dtos;
namespace WebApplicationApi.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
        }
    }
}
