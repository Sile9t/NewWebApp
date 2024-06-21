using AutoMapper;
using NewWebApp.Dto;
using NewWebApp.Models;

namespace NewWebApp.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();
        }
    }
}
