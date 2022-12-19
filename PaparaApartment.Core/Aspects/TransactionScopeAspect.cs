using System;
using System.Transactions;
using Castle.DynamicProxy;
using PaparaApartment.Core.Utilities.Interceptors;

namespace PaparaApartment.Core.Aspects
{
    public class TransactionScopeAscpect:MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();

                    transactionScope.Complete();
                }
                catch (Exception e)
                {
                    transactionScope.Dispose();

                    OnException(invocation, e);
                    throw;
                }
            }
        }
    }
}
