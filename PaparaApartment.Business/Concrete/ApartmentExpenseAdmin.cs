using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.ApartmentExpense;
using System;
using System.Collections.Generic;
using AutoMapper;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Business.Aspects;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Data.Abstract;

namespace PaparaApartment.Business.Concrete
{
    public class ApartmentExpenseAdmin : IApartmentExpenseService
    {
        private IApartmentExpenseDal _apartmentExpenseDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private IApartmentService _apartmentAdmin;

        public ApartmentExpenseAdmin(IApartmentExpenseDal apartmentExpenseDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, IApartmentService apartmentAdmin)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _apartmentAdmin = apartmentAdmin;
            _apartmentExpenseDal = apartmentExpenseDal;

        }

        public IResult Add(ApartmentExpenseAddDto apartmentExpenseAddDto)
        {
            var expenseCheck = _apartmentExpenseDal.Any(x => x.ApartmentId == apartmentExpenseAddDto.ApartmentId && x.ExpenseId == apartmentExpenseAddDto.ExpenseId);
            if (expenseCheck)
            {
                return new ErrorResult(Messages.ApartmentExpenseAlreadyExist);
            }

            var newApartmentExpense = _mapper.Map<ApartmentExpense>(apartmentExpenseAddDto);
            newApartmentExpense.DidPay = false;
            newApartmentExpense.IsActive = true;
            newApartmentExpense.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newApartmentExpense.Idate = DateTime.Now;
            _apartmentExpenseDal.Add(newApartmentExpense);
            return new SuccessResult(Messages.ApartmentExpenseAdded);
        }

        public IResult Pay(int expenseId)
        {
            throw new NotImplementedException();
        }

        [SecuredOperation("admin")]
        public IDataResult<List<ApartmentExpenseViewDto>> GetUnPaidPayments(int apartmentId)
        {

            var unpaidPayments = _apartmentExpenseDal.GetUnPaidPayments(x => x.ApartmentId == apartmentId);
            if (unpaidPayments is null)
            {
                return new SuccessDataResult<List<ApartmentExpenseViewDto>>(Messages.UnpaidPaymentsNotFound);
            }

            return new SuccessDataResult<List<ApartmentExpenseViewDto>>(unpaidPayments);
        }


        [SecuredOperation("admin")]
        public IDataResult<List<ApartmentExpenseViewDto>> GetPaidPayments(int apartmentId)
        {
            if (apartmentId < 0)
            {
                apartmentId = _apartmentAdmin.GetIdByResidentId(_httpContextAccessor.HttpContext.User.GetLoggedUserId());
            }

            var paidPayments = _apartmentExpenseDal.GetPaidPayments(x => x.ApartmentId == apartmentId);
            if (paidPayments is null)
            {
                return new SuccessDataResult<List<ApartmentExpenseViewDto>>(Messages.PaidPaymentsNotFound);
            }

            return new SuccessDataResult<List<ApartmentExpenseViewDto>>(paidPayments);
        }

        public IDataResult<List<ApartmentExpenseViewDto>> GetMyUnPaidPayments()
        {
            var apartmentId = _apartmentAdmin.GetIdByResidentId(_httpContextAccessor.HttpContext.User.GetLoggedUserId());
            var result = GetUnPaidPayments(apartmentId);
            return new SuccessDataResult<List<ApartmentExpenseViewDto>>(result.Data);
        }

        public IDataResult<List<ApartmentExpenseViewDto>> GetMyPaidPayments()
        {
            var apartmentId = _apartmentAdmin.GetIdByResidentId(_httpContextAccessor.HttpContext.User.GetLoggedUserId());
            var result = GetPaidPayments(apartmentId);
            return new SuccessDataResult<List<ApartmentExpenseViewDto>>(result.Data);
        }

        public bool IsFullyPaid(int expenseId)
        {
            var check = _apartmentExpenseDal.Any(x => x.ExpenseId == expenseId && x.DidPay == false);
            if (check)
            {
                return false;
            }

            return true;
        }
    }
}
