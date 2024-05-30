using AutoMapper;
using Talabat.Apis.DTOS;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductToReturnDto>()
                .ForMember(d=>d.Brand , O=>O.MapFrom(S=>S.ProductBrand.Name))
                .ForMember(d=>d.Category,O=>O.MapFrom(s=>s.ProductCategory.Name))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
        }
    }
}
