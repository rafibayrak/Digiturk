using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Business.Services.IServices;

namespace MovieApp.Business.Aspects
{
    public class AuthorizeVerifyTokenAttribute : AuthorizeAttribute, IAuthorizationFilter
    {

        public AuthorizeVerifyTokenAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var service = context.HttpContext.RequestServices.GetService<IAuthenticateService>();
            if (!service.IsTokenValid(context.HttpContext.User.Identity.Name))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
