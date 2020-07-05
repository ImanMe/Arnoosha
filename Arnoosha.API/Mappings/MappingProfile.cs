using Arnoosha.API.Dtos;
using Arnoosha.Core.Entities;
using AutoMapper;

namespace Arnoosha.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductBrand, o => o.MapFrom(n => n.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(n => n.ProductType.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
