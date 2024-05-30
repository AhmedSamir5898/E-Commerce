using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.IRepository;

namespace Talabat.Apis.Controllers
{
   
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepo,
            IMapper mapper)
        {
           _basketRepo = basketRepo;
           _mapper = mapper;
        }


        [HttpDelete]
        public async Task DeletBasketAsync(string BasketId)
        {
           await _basketRepo.DeleteBasketAsync(BasketId);
        }
        [HttpGet]
        public async Task<ActionResult <CustomerBasket>> GetBasketAsync (string basketId)
        {
             var basket =await _basketRepo.GetBasketAsync(basketId);
             return Ok( basket ?? new CustomerBasket(basketId));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateOrCreateBasket ([FromBody] CustomerBasketDto Basket)
        {
            var MappedBAsket = _mapper.Map<CustomerBasketDto , CustomerBasket>(Basket);
            var createdorUpdated = await _basketRepo.CreateOrUpdateAsync(MappedBAsket);
            if(createdorUpdated is null) return BadRequest(new ApiResponse(400));

            return  Ok(createdorUpdated);
        }
    }
}
