using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using MovieApp.Business.Extensions.Dependencies;
using MovieApp.Business.Interseptors;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace MovieApp.Business.Aspects
{
    public class MemoryCacheAspect : MethodInterception
    {
        private IMemoryCache _cacheManager;
        private int _duration;
        public MemoryCacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceInjection.ServiceProvider.GetService<IMemoryCache>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            object cachedData;
            if (_cacheManager.TryGetValue(key, out cachedData))
            {
                invocation.ReturnValue = cachedData;
                return;
            }

            invocation.Proceed();
            _cacheManager.Set(key, invocation.ReturnValue, TimeSpan.FromMinutes(_duration));
        }
    }
}
