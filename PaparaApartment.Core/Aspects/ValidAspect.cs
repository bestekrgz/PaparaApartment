using Castle.DynamicProxy;
using FluentValidation;
using System;
using System.Linq;
using PaparaApartment.Core.CrossCuttingConcern;
using PaparaApartment.Core.Utilities.Interceptors;
using PaparaApartment.Core.Utilities.Messages;

namespace PaparaApartment.Core.Aspects
{
    public class ValidAspect : MethodInterception
    {
        private Type _validatorType;

        public ValidAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new Exception(MessagesAspect.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
          
            var validator = (IValidator)Activator.CreateInstance(_validatorType);

 
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidatorTool.Validate(validator, entity);
            }

        }

    }
}
