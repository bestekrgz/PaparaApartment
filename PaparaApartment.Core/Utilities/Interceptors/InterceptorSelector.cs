using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;


namespace PaparaApartment.Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBase>(true).ToList();

            var methodAttributes =
                type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBase>(true);

            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
