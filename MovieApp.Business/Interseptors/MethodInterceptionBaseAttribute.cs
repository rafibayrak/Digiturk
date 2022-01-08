using Castle.DynamicProxy;
using System;

namespace MovieApp.Business.Interseptors
{
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public virtual void Intercept(IInvocation invocation)
        {
        }
    }
}
