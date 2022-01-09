using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Extensions;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Dtos;
using System.Threading.Tasks;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authService;

        public AuthenticateController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Cookie Authentication kullanildi
        /// LoginDto icin ValidationAspect kullanildi 
        /// ValidationAspect icerisinde FluentValidation ile kontroller yapildi
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            var userAuthDto = await _authService.SignInCookieAsync(loginDto);
            return this.NotFoundOrOk(userAuthDto, "UserName or Password not valid");
        }

        /// <summary>
        /// Olusturulan Session sonlandirilir
        /// </summary>
        /// <returns></returns>
        [HttpGet("signout")]
        public async Task<IActionResult> Signout()
        {
            await _authService.SignOutCookie();
            return NoContent();
        }
    }
}
