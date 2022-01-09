using MovieApp.Data.Dtos;
using System.Threading.Tasks;

namespace MovieApp.Business.Services.IServices
{
    public interface IAuthenticateService
    {
        public Task<BaseAuthDto> SignInCookieAsync(LoginDto loginDto);
        public Task SignOutCookie();
    }
}
