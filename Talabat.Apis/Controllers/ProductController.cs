using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Apis.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.IRepository;
using Talabat.Core.Specifications.Product_Specs;

namespace Talabat.Apis.Controllers
{
   
    public class ProductController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categoriesRepo;
        private readonly ApiService _apiService;
        private readonly IMapper _mapper;

        public ProductController(IGenericRepository<Product> ProductRepo ,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductCategory> categoriesRepo,ApiService apiService, IMapper mapper)
        {
            _productRepo = ProductRepo;
            _brandRepo = brandRepo;
            _categoriesRepo = categoriesRepo;
            _apiService = apiService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> Get([FromQuery]ProductSpecParam specParam)
        {
            var spec = new ProductIncludesBrandsandCategory(specParam);
            var Products = await _productRepo.GetAllWithSpecAsync(spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var CountSpec = new ProducWithFiltarationForCountSpecifcation(specParam);
            var count = await _productRepo.GetCountWithSpec(CountSpec)  ;
            return Ok(new Pagination<ProductToReturnDto> (specParam.PageIndex,specParam.PageSize,count,Data));
        }
                
        [HttpGet("{id}")]

        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec =new ProductIncludesBrandsandCategory(id);
            var product = await _productRepo.GetwithSpecAsync(spec);
            if(product == null)
                return NotFound(new ApiResponse(404));
          
            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }
        [HttpGet("brnads")] // :Api/product/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrnads()
        {
            var brnads =await _brandRepo.GetAllAsync();
            return Ok(brnads);
        }
        [HttpGet("category")] // : Api/Product/category
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories =await _categoriesRepo.GetAllAsync();
            return Ok(categories);
        }

        

        
    }
}
