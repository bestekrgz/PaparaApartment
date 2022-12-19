
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entities.Dtos.Payment;
using PaparaApartment.Entity.Concrete;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{
    public interface IPaymentService
    {
        IResult Add(Payment addPayment);

        IDataResult<List<PaymentViewDto>> GetAll();
        IDataResult<List<PaymentViewDto>> GetByApartmentId(int apartmentId);
    }
}
