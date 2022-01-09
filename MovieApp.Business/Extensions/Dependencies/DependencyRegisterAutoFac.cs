using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using MovieApp.Business.Aspects;
using MovieApp.Business.Interseptors;
using MovieApp.Business.Services;
using MovieApp.Business.Services.IServices;
using MovieApp.DataAccess.Repositories;
using MovieApp.DataAccess.Repositories.IRepositories;
using System;

namespace MovieApp.Business.Extensions.Dependencies
{
    public class DependencyRegisterAutoFac : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<UserRepository>().As<IUserRepository>();
            //builder.RegisterType<MovieRepository>().As<IMovieRepository>();
            //builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            //builder.RegisterType<AuthService>().As<IAuthService>();
            //builder.RegisterType<MovieService>().As<IMovieService>();
            //builder.RegisterType<CategoryService>().As<ICategoryService>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions() {
                    Selector = new AspectInterseptorSelector()
                }).InstancePerLifetimeScope();
        }
    }
}
