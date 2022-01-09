using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieApp.Business.Aspects;
using MovieApp.Business.FluentValidators;
using MovieApp.Business.Helpers;
using MovieApp.Business.Services.IServices;
using MovieApp.Data.Core;
using MovieApp.Data.Dtos;
using MovieApp.DataAccess.Repositories.IRepositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public UserAuthDto SignIn(LoginDto loginDto)
        {
            var user = _userRepository.GetUserByUserName(loginDto.UserName);
            if (user == null || !PaswordHash.VerifyPassword(loginDto.Password, user.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string generatedToken = tokenHandler.WriteToken(token);
            _userRepository.UpdateToken(user.UserName, generatedToken);
            return new UserAuthDto
            {
                UserName = user.UserName,
                Token = generatedToken
            };
        }

        [LoggerAspect]
        public void SignOut(string userName)
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            _userRepository.UpdateToken(userName, string.Empty);
        }

        [LoggerAspect]
        public bool IsTokenValid(string userName)
        {
            var user = _userRepository.GetUserByUserName(userName);
            return user != null ? !string.IsNullOrEmpty(user.Token) : false;
        }
    }
}
