using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProducWithFiltarationForCountSpecifcation :BaseSpecification<Product>
    {
        public ProducWithFiltarationForCountSpecifcation(ProductSpecParam sepcparam)
            : base(p =>
            (string.IsNullOrEmpty(sepcparam.search) || p.Name.ToLower().Contains(sepcparam.search)) &&
            (!sepcparam.CategoryId.HasValue || sepcparam.CategoryId == p.CategoryId) &&
            (!sepcparam.BrandId.HasValue || sepcparam.BrandId == p.BrandId))
        {
            
        }
    }
}
