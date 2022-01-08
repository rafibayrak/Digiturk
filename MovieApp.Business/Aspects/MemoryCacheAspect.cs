using Castle.DynamicProxy;
using MovieApp.Business.Interseptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Aspects
{
    public class MemoryCacheAspect : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            //var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //var entities = invocation.Arguments.Where(x => x.GetType() == entityType);
            //foreach (var entity in entities)
            //{
            //    ValidationTool.Validate(validator, entity);
            //}
        }
    }
}
