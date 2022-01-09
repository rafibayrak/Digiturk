using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MovieApp.Business.Aspects;
using MovieApp.Business.FluentValidators;
using MovieApp.Business.Helpers;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Core;
using MovieApp.Data.Dtos;
using MovieApp.DataAccess.Repositories.IRepositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieApp.Business.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticateService(IOptions<AppSettings> appSettings, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [ValidationAspect(typeof(LoginValidator))]
        [LoggerAspect]
        public async Task<BaseAuthDto> SignInCookieAsync(LoginDto loginDto)
        {
            var user = _userRepository.GetUserByUserName(loginDto.UserName);
            if (user == null || !PaswordHash.VerifyPassword(loginDto.Password, user.Password))
            {
                return null;
            }

            List<Claim> userClaims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return new BaseAuthDto
            {
                UserName = user.UserName,
                FullName = user.FullName
            };
        }

        public async Task SignOutCookie()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
