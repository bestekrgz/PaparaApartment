using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entities.Dtos.Expense;
using System;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{
    public interface IExpenseService
    {
        void Add(ExpenseAddForAllDto expenseAddDto);
        IResult Update(ExpenseUpdateDto expenseUpdateDto);
        IResult Delete(int expenseId);
        IResult AddExpenseForAll(ExpenseAddForAllDto expenseAddDto);
        IResult AddExpenseForOne(ExpenseAddForOneDto expenseAddForOneDto);
        int GetLastExpenseId();
        IDataResult<List<ExpenseViewDto>> GetList();
        IDataResult<List<ExpenseViewDto>> GetListFilterDate(DateTime dataTime);
    }
}
