using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Services;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Business.Extensions.Dependencies
{
    public static class ServiceInjection
    {
        public static void CustomServiceInjection(this IServiceCollection services) {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ServiceInjection));
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<ICategoryService, CategoryService>();
        }
    }
}
