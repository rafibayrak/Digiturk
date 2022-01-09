using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using MovieApp.Business.Extensions.Dependencies;
using MovieApp.Business.Interseptors;
using NLog;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace MovieApp.Business.Aspects
{
    public class LoggerAspect : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        public LoggerAspect()
        {
            _httpContextAccessor = ServiceInjection.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error($"[UserName: {_httpContextAccessor.HttpContext.User.Identity.Name}] [Calling:{name}] [Args:{args}] [Done: result was {invocation.ReturnValue}] [ErrorMessage: {e.Message}] [StackTrace: {e.StackTrace}]");
        }

        protected override void OnAfter(IInvocation invocation)
        {
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info($"[UserName: {_httpContextAccessor.HttpContext.User.Identity.Name}] [Calling:{name}] [Args:{args}] [Done: result was {invocation.ReturnValue}]");
        }
    }
}
