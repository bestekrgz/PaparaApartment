using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaparaApartment.Entities.Dtos.Expense;

namespace PaparaApartment.Data.Abstract
{
    public interface IExpenseDal : IEntityRepository<Expense>
    {
        int GetLastMessageId();
        List<ExpenseViewDto> GetListWithPaymentInfo(Expression<Func<ExpenseViewDto, bool>> filter = null);
    }
}
