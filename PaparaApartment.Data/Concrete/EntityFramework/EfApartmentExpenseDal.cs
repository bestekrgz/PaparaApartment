using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Data.Context;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.ApartmentExpense;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;


namespace PaparaApartment.Data.Concrete.EntityFramework
{
    public class EfApartmentExpenseDal : EfEntityRepositoryBase<ApartmentExpense, PaparaApartmentDbContext>, IApartmentExpenseDal
    {
        public List<ApartmentExpenseViewDto> GetUnPaidPayments(Expression<Func<ApartmentExpenseViewDto, bool>> filter = null)
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = (from apartmentExpense in context.ApartmentExpenses
                    join apartment in context.Apartments
                        on apartmentExpense.ApartmentId equals apartment.Id
                    join expense in context.Expenses
                        on apartmentExpense.ExpenseId equals expense.Id
                    join expenseType in context.ExpenseTypes
                        on expense.TypeId equals expenseType.Id
                    where apartmentExpense.IsActive == true && apartmentExpense.DidPay == false
                    select new ApartmentExpenseViewDto()
                    {
                        Id = apartmentExpense.Id,
                        ApartmentId = apartmentExpense.ApartmentId,
                        ExpenseId = apartmentExpense.ExpenseId,
                        Type = expenseType.Name,
                        ExpenseName = expense.Name,
                        Amount = expense.Amount,
                        Date = expense.Date
                    }).OrderBy(x=>x.Date);
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public List<ApartmentExpenseViewDto> GetPaidPayments(Expression<Func<ApartmentExpenseViewDto, bool>> filter = null)
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = (from apartmentExpense in context.ApartmentExpenses
                    join apartment in context.Apartments
                        on apartmentExpense.ApartmentId equals apartment.Id
                    join expense in context.Expenses
                        on apartmentExpense.ExpenseId equals expense.Id
                    join expenseType in context.ExpenseTypes
                        on expense.TypeId equals expenseType.Id
                    where apartmentExpense.DidPay == true
                    select new ApartmentExpenseViewDto()
                    {
                        Id = apartmentExpense.Id,
                        ApartmentId = apartmentExpense.ApartmentId,
                        ExpenseId = apartmentExpense.ExpenseId,
                        Type = expenseType.Name,
                        ExpenseName = expense.Name,
                        Amount = expense.Amount,
                        Date = expense.Date
                    }).OrderByDescending(x => x.Date);
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
