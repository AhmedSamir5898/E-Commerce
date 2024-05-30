using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductIncludesBrandsandCategory :BaseSpecification<Product>
    {
        public ProductIncludesBrandsandCategory(ProductSpecParam specParam) 
            :base(p =>
                 (string.IsNullOrEmpty(specParam.search) || p.Name.ToLower().Contains(specParam.search))&&
                 (!specParam.BrandId.HasValue || p.BrandId == specParam.BrandId) &&
                (!specParam.CategoryId.HasValue || p.CategoryId == specParam.CategoryId)
                 )
        {
            Includes();
            if (!string.IsNullOrEmpty(specParam.sort))
            {
                switch (specParam.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }
            else
            AddOrderBy(p => p.Name);
            ApplyPagination((specParam.PageIndex - 1) * specParam.PageSize, specParam.PageSize);
        }
        public ProductIncludesBrandsandCategory(int id ) :base(P=>P.Id==id)
        {
            Includes();
        }   

        private void Includes()
        {
            base.Includes.Add(p => p.ProductBrand);
            base.Includes.Add(P => P.ProductCategory);
        }
        
   }
}
