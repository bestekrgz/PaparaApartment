using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Business.Abstract
{
    public interface IExpenseTypeService
    {
        IDataResult<List<ExpenseTypeViewDto>> GetAll();

        IResult Add(ExpenseTypeAddDto expenseTypeAddDto);

        IResult Delete(int expenseTypeId);

        IResult Update(ExpenseTypeUpdateDto expenseTypeUpdateDto);
    }
}
