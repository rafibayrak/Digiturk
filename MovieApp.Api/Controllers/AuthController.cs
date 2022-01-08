using Microsoft.AspNetCore.Mvc;
using MovieApp.Business.Aspects;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Dtos;
using System;

namespace MovieApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signin")]
        [Logger]
        public IActionResult SignIn(LoginDto loginDto)
        {
            var userAuthDto = _authService.SignIn(loginDto);
            if (userAuthDto == null)
            {
                throw new Exception("User not found");
            }

            return Ok(userAuthDto);
        }

        [HttpGet("signOut")]
        [Logger]
        [AuthorizeVerifyToken]
        public IActionResult SignOut()
        {
            _authService.SignOut(HttpContext.User.Identity.Name);
            return NoContent();
        }
    }
}
