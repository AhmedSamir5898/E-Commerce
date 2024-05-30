using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications.Product_Specs
{
    public class ProductSpecParam
    {
        public const int MaxPageSize = 10;

        private string? Search;

        public string? search
        {
            get { return Search; }
            set { Search = value?.ToLower(); }
        }

        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set;}

        public int PageIndex { get; set; } = 1;
        
        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? MaxPageSize :value; }
        }

    
    }
}
