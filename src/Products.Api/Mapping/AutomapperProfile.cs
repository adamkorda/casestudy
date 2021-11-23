using AutoMapper;

using Products.Api.Data.Entities;
using Products.Api.Models.Responds;

namespace Products.Api.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Product, ProductRespond>();
        }
    }
}
