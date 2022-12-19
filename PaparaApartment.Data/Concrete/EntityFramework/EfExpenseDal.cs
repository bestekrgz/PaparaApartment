
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PaparaApartment.Data.Context;
using PaparaApartment.Entities.Dtos.Expense;
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Data.Abstract;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfExpenseDal : EfEntityRepositoryBase<Expense, PaparaApartmentDbContext>, IExpenseDal
    {
        public int GetLastMessageId()
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var id = context.Set<Expense>().ToList().Last().Id;
                return id;
            }
        }

        public List<ExpenseViewDto> GetListWithPaymentInfo(Expression<Func<ExpenseViewDto, bool>> filter = null)
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = (from expense in context.Expenses
                              join expenseType in context.ExpenseTypes
                              on expense.TypeId equals expenseType.Id
                              where expense.IsActive == true
                              select new ExpenseViewDto()
                              {
                                  Id = expense.Id,
                                  TypeName = expenseType.Name,
                                  Name = expense.Name,
                                  Amount = expense.Amount,
                                  Date = expense.Date,
                                  PaidCount = (from apartmentExpense in context.ApartmentExpenses
                                               where apartmentExpense.ExpenseId == expense.Id && apartmentExpense.DidPay == true
                                               select apartmentExpense.Id).Count(),
                                  UnPaidCount = (from apartmentExpense in context.ApartmentExpenses
                                                 where apartmentExpense.ExpenseId == expense.Id && apartmentExpense.DidPay == false
                                                 select apartmentExpense.Id).Count()

                              }).OrderBy(x => x.Date);
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
