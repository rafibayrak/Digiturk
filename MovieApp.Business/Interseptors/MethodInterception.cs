﻿using Castle.DynamicProxy;
using System;
using System.Threading.Tasks;

namespace MovieApp.Business.Interseptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)
        { }

        protected virtual void OnAfter(IInvocation invocation)
        { }

        protected virtual void OnException(IInvocation invocation, Exception e)
        { }

        protected virtual void OnSuccess(IInvocation invocation)
        { }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
                if (invocation.ReturnValue is Task returnValueTask)
                {
                    returnValueTask.GetAwaiter().GetResult();
                }

                if (invocation.ReturnValue is Task task && task.Exception != null)
                {
                    throw task.Exception;
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);
        }
    }
}
