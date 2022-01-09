using Castle.DynamicProxy;
using MovieApp.Business.Interseptors;
using NLog;
using System.Linq;

namespace MovieApp.Business.Aspects
{
    public class LoggerAspect : MethodInterception
    {
        protected override void OnAfter(IInvocation invocation)
        {
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            var logger = LogManager.GetCurrentClassLogger();
            logger.Info($"[Calling:{name}] [Args:{args}] [Done: result was {invocation.ReturnValue}]");
        }
    }
}
