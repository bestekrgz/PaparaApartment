using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entities.Dtos.Expense;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.ApartmentExpense;
using System;
using System.Collections.Generic;
using PaparaApartment.Core.Aspects;


namespace PaparaApartment.Business.Concrete
{
    public class ExpenseAdmin : IExpenseService
    {
        private IExpenseDal _expenseDal;
        private IMapper _mapper;
        private IApartmentService _apartmentAdmin;
        private IHttpContextAccessor _httpContextAccessor;
        private IApartmentExpenseService _apartmentExpenseAdmin;

        public ExpenseAdmin(IExpenseDal expenseDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, IApartmentService apartmentAdmin, IApartmentExpenseService apartmentExpenseAdmin)
        {
            _expenseDal = expenseDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _apartmentAdmin = apartmentAdmin;
            _apartmentExpenseAdmin = apartmentExpenseAdmin;
        }

        public void Add(ExpenseAddForAllDto expenseAddDto)
        {
            var newExpense = _mapper.Map<Expense>(expenseAddDto);
            newExpense.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newExpense.Idate = DateTime.Now;
            newExpense.IsActive = true;
            _expenseDal.Add(newExpense);
        }

        public int GetLastExpenseId()
        {
            return _expenseDal.GetLastMessageId();
        }

        public IDataResult<List<ExpenseViewDto>> GetList()
        {
            var expenseList = _expenseDal.GetListWithPaymentInfo();
            if (expenseList is null)
            {
                return new SuccessDataResult<List<ExpenseViewDto>>(Messages.ExpenseListNotFound);
            }

            return new SuccessDataResult<List<ExpenseViewDto>>(expenseList);
        }

        public IDataResult<List<ExpenseViewDto>> GetListFilterDate(DateTime dateTime)
        {
            var expenseList = _expenseDal.GetListWithPaymentInfo(x => x.Date.Month == dateTime.Month && x.Date.Year == dateTime.Year);
            if (expenseList is null)
            {
                return new SuccessDataResult<List<ExpenseViewDto>>(Messages.ExpenseFilterListNotFound);
            }

            return new SuccessDataResult<List<ExpenseViewDto>>(expenseList);
        }


        public IResult Update(ExpenseUpdateDto expenseUpdateDto)
        {
            var expense = _expenseDal.Get(x => x.Id == expenseUpdateDto.Id);

            if (expense is null)
            {
                return new ErrorResult(Messages.ExpenseNotFound);
            }

            expense = _mapper.Map(expenseUpdateDto, expense);
            expense.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            expense.Udate = DateTime.Now;
            _expenseDal.Update(expense);
            return new SuccessResult(Messages.ExpenseUpdated);
        }

        public IResult Delete(int expenseId)
        {
            var expense = _expenseDal.Get(x => x.Id == expenseId);

            if (expense is null)
            {
                return new ErrorResult(Messages.ExpenseNotFound);
            }

            if (!_apartmentExpenseAdmin.IsFullyPaid(expenseId))
            {
                return new ErrorResult(Messages.ExpenseCanNotBeRemoved);
            }

            expense.IsActive = false;
            expense.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            expense.Udate = DateTime.Now;
            _expenseDal.Update(expense);

            return new SuccessResult(Messages.ExpenseRemoved);
        }

        [TransactionScopeAscpect]
        public IResult AddExpenseForAll(ExpenseAddForAllDto expenseAddDto)
        {

            Add(expenseAddDto);

            var expenseId = GetLastExpenseId();

            var apartmentIdList = _apartmentAdmin.GetIdList();

            foreach (var apartment in apartmentIdList.ToArray())
            {
                _apartmentExpenseAdmin.Add(new ApartmentExpenseAddDto()
                {
                    ApartmentId = apartment,
                    ExpenseId = expenseId,
                });
            }

            return new SuccessResult(Messages.ExpenseAddedForAll);
        }

        public IResult AddExpenseForOne(ExpenseAddForOneDto expenseAddForOneDto)
        {
            var newExpense = _mapper.Map<ExpenseAddForAllDto>(expenseAddForOneDto);

            Add(newExpense);

            var expenseId = GetLastExpenseId();

            var apartmentId = expenseAddForOneDto.ApartmentId;

            _apartmentExpenseAdmin.Add(new ApartmentExpenseAddDto()
            {
                ApartmentId = apartmentId,
                ExpenseId = expenseId,
            });

            return new SuccessResult();
        }
    }
}
