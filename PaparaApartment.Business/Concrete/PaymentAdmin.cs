using AutoMapper;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entities.Dtos.Payment;
using PaparaApartment.Entity.Concrete;
using System.Collections.Generic;


namespace PaparaApartment.Business.Concrete
{
    public class PaymentAdmin : IPaymentService
    {
        private IPaymentDal _paymentDal;
        private IMapper _mapper;

        public PaymentAdmin(IPaymentDal paymentDal, IMapper mapper)
        {
            _paymentDal = paymentDal;
            _mapper = mapper;
        }

        public IResult Add(Payment addPayment)
        {
            _paymentDal.Add(addPayment);
            return new SuccessResult();
        }

        public IDataResult<List<PaymentViewDto>> GetAll()
        {
            var paymentList = _paymentDal.GetList();
            var paymentViewList = _mapper.Map<List<PaymentViewDto>>(paymentList);
            return new SuccessDataResult<List<PaymentViewDto>>(paymentViewList);
        }

        public IDataResult<List<PaymentViewDto>> GetByApartmentId(int apartmentId)
        {
            var paymentList = _paymentDal.GetList(x => x.ApartmentId == apartmentId);
            var paymentViewList = _mapper.Map<List<PaymentViewDto>>(paymentList);
            return new SuccessDataResult<List<PaymentViewDto>>(paymentViewList);
        }

    }
}
