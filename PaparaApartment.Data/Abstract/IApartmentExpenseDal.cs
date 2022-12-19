using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaparaApartment.Entity.Dtos.ApartmentExpense;

namespace PaparaApartment.Data.Abstract
{
    public interface IApartmentExpenseDal : IEntityRepository<ApartmentExpense>
    {
        List<ApartmentExpenseViewDto> GetUnPaidPayments(Expression<Func<ApartmentExpenseViewDto, bool>> filter = null);
        List<ApartmentExpenseViewDto> GetPaidPayments(Expression<Func<ApartmentExpenseViewDto, bool>> filter = null);
    }
}
