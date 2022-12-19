
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entities.Dtos.ExpenseType;
using System.Collections.Generic;

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
