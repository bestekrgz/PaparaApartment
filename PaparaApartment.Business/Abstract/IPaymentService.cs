using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Business.Abstract
{
    public interface IPaymentService
    {
        IResult Add(Payment addPayment);

        IDataResult<List<PaymentViewDto>> GetAll();
        IDataResult<List<PaymentViewDto>> GetByApartmentId(int apartmentId);
    }
}
