using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOS;
using Talabat.Apis.Errors;
using Talabat.Core.Entities.Identities;

namespace Talabat.Apis.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login([FromBody]LogInDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return BadRequest(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync( user,model.Password,false);
            if (result.Succeeded is false)
                return BadRequest(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName=user.DisPlayName,
                Email=user.Email,
                Token = "this will be token"
            });

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto model)
        {
            var user = new AppUser()
            {
                DisPlayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split("@")[0],
                PhoneNumber = model.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded is false) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisPlayName,
                Email = user.Email,
                Token = "this will be token"
            });
        }
    }
}
