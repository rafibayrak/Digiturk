using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Services;
using MovieApp.Business.Services.IServices;
using System;

namespace MovieApp.Business.Extensions.Dependencies
{
    public static class ServiceInjection
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static void CustomServiceInjection(this IServiceCollection services) {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(ServiceInjection));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<ICategoryService, CategoryService>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
