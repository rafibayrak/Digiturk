using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Services;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Business.Extensions.Dependencies
{
    public static class ServiceInjection
    {
        public static void CustomServiceInjection(this IServiceCollection services) {
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ServiceInjection));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }
    }
}
