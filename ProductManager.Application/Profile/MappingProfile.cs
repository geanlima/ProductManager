using AutoMapper;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Entities;

namespace ProductManager.Application.Profile;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Products, ProductDTO>().ReverseMap();
        CreateMap<IEnumerable<Products>, List<ProductDTO>>().ReverseMap();
    }
}
