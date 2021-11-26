using AutoMapper;

using Products.Api.Data.Core;
using Products.Api.Data.Entities;
using Products.Api.Dtos;

namespace Products.Api.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<PaginationMetadata, PaginationMetadataDto>();
        }
    }
}
