using Castle.DynamicProxy;
using MovieApp.Business.Interseptors;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Business.Aspects
{
    public class MemoryCacheAspect : MethodInterception
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>();

        protected override void OnBefore(IInvocation invocation)
        {
            var name = $"{invocation.Method.DeclaringType}_{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            var cacheKey = $"{name}|{args}";

            cache.TryGetValue(cacheKey, out object returnValue);
            if (returnValue == null)
            {
                invocation.Proceed();
                returnValue = invocation.ReturnValue;
                cache.Add(cacheKey, returnValue);
            }
            else
            {
                invocation.ReturnValue = returnValue;
            }
        }
    }
}
