using Microsoft.Extensions.DependencyInjection;
using MovieApp.DataAccess.Repositories;
using MovieApp.DataAccess.Repositories.IRepositories;

namespace MovieApp.Business.Extensions.Dependencies
{
    public static class RepositoryInjection
    {
        public static void CustomRepositoryInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
