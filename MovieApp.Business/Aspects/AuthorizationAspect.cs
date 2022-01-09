using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using MovieApp.Business.Extensions.Dependencies;
using MovieApp.Business.Interseptors;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Extensions;
using System;

namespace MovieApp.Business.Aspects
{
    public class AuthorizationAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public AuthorizationAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor = ServiceInjection.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRoles(_roles))
            {
                throw new Exception("You are not authorized!");
            }
        }
    }
}
