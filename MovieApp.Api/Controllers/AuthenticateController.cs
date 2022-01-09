using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Aspects;
using MovieApp.Business.Extensions;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Dtos;
using System;

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

        [HttpPost("signin")]
        //[Logger]
        public IActionResult SignIn(LoginDto loginDto)
        {
            var userAuthDto = _authService.SignIn(loginDto);
            if (userAuthDto == null)
            {
                throw new Exception("User not found");
            }

            return this.NotFoundOrOk(userAuthDto);
        }

        [HttpGet("signout")]
        [AuthorizeVerifyToken]
        public IActionResult SignOut()
        {
            _authService.SignOut(HttpContext.User.Identity.Name);
            return NoContent();
        }
    }
}
