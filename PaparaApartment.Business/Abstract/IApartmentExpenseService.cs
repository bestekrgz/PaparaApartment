using System.Collections.Generic;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.ApartmentExpense;

namespace PaparaApartment.Business.Abstract
{
    public interface IApartmentExpenseService

    {
        IResult Add(ApartmentExpenseAddDto apartmentExpenseAddDto);
        IResult Pay(int expenseId);
        IDataResult<List<ApartmentExpenseViewDto>> GetUnPaidPayments(int apartmentId);
        IDataResult<List<ApartmentExpenseViewDto>> GetPaidPayments(int apartmentId);
        IDataResult<List<ApartmentExpenseViewDto>> GetMyUnPaidPayments();
        IDataResult<List<ApartmentExpenseViewDto>> GetMyPaidPayments();
        bool IsFullyPaid(int expenseId);
    }

}

