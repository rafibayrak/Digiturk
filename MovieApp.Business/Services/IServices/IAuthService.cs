using MovieApp.Data.Dtos;

namespace MovieApp.Business.Services.IServices
{
    public interface IAuthService
    {
        public UserAuthDto SignIn(LoginDto loginDto);
        public void SignOut(string userName);
        public bool IsTokenValid(string userName);
    }
}
