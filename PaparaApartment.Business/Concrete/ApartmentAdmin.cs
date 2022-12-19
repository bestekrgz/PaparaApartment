using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Dtos.User;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.Apartment;
using System;
using System.Collections.Generic;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Business.Aspects;
using PaparaApartment.Data.Abstract;

namespace PaparaApartment.Business.Concrete
{
    public class ApartmentAdmin : IApartmentService
    {
        private IApartmentDal _apartmentDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public ApartmentAdmin(IApartmentDal apartmentDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _apartmentDal = apartmentDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDataResult<List<ApartmentViewDto>> GetAll()
        {
            var result = _apartmentDal.GetListWithDetails();
            return new SuccessDataResult<List<ApartmentViewDto>>(result);

        }

        public IDataResult<List<UserViewDto>> GetAllResident()
        {
            var userList = _apartmentDal.GetResidentList();

            return new SuccessDataResult<List<UserViewDto>>(userList);
        }

        public List<int> GetIdList()
        {
            var idList = _apartmentDal.GetApartmentIdList();
            return idList;
        }

        public int GetIdByResidentId(int residentId)
        {
            var isHirer = IsHirer(residentId);

            return isHirer ? _apartmentDal.Get(x => x.HirerId == residentId).Id : _apartmentDal.Get(x => x.OwnerId == residentId).Id;
        }

        public bool IsHirer(int residentId)
        {
            var isHirer = _apartmentDal.Any(x => x.HirerId == residentId);
            return isHirer;
        }

        public IResult Add(ApartmentAddDto apartmentAddDto)
        {
            var apartmentCheck = _apartmentDal.Any(x =>
                x.BlockId == apartmentAddDto.BlockId && x.DoorNumber == apartmentAddDto.DoorNumber);

            if (apartmentCheck)
            {
                return new ErrorResult(Messages.ApartmentAlreadyExist);
            }

            var newApartment = _mapper.Map<Apartment>(apartmentAddDto);
            newApartment.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newApartment.Idate = DateTime.Now;
            _apartmentDal.Add(newApartment);

            return new SuccessResult(Messages.ApartmentAdded);
        }

        public IResult Update(ApartmentUpdateDto apartmentUpdateDto)
        {
            var apartment = _apartmentDal.Get(x => x.Id == apartmentUpdateDto.Id);

            if (apartment is null)
            {
                return new ErrorResult(Messages.ApartmentNotFound);
            }

            apartment = _mapper.Map(apartmentUpdateDto, apartment);
            apartment.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            apartment.Udate = DateTime.Now;
            _apartmentDal.Update(apartment);

            return new SuccessResult(Messages.ApartmentUpdated);
        }

        public IResult UpdateUser(ApartmentUserUpdateDto apartmentUserUpdateDto)
        {
            var apartment = _apartmentDal.Get(x => x.Id == apartmentUserUpdateDto.ApartmentId);

            if (apartment is null)
            {
                return new ErrorResult(Messages.ApartmentNotFound);
            }

            if (apartmentUserUpdateDto.IsHirer)
            {
                if (apartmentUserUpdateDto.IsResident)
                {
                    apartment.HirerId = apartmentUserUpdateDto.UserId;
                    apartment.Status = true;
                }
                else
                {
                    apartment.HirerId = null;
                    apartment.Status = true;
                }
            }
            else
            {
                apartment.OwnerId = apartmentUserUpdateDto.UserId;
                if (apartmentUserUpdateDto.IsResident)
                {
                    apartment.Status = true;
                }
            }

            apartment.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            apartment.Udate = DateTime.Now;
            _apartmentDal.Update(apartment);

            return new SuccessResult(Messages.ApartmentUpdated);
        }


        public IResult UpdateStatus(int apartmentId, bool status)
        {
            var apartment = _apartmentDal.Get(x => x.Id == apartmentId);

            if (apartment is null)
            {
                return new ErrorResult(Messages.ApartmentNotFound);
            }

            apartment.Status = status;
            apartment.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            apartment.Udate = DateTime.Now;
            _apartmentDal.Update(apartment);
            return new SuccessResult(Messages.ApartmentUpdated);
        }

        public IResult Delete(int apartmentId)
        {
            var apartment = _apartmentDal.Get(x => x.Id == apartmentId);

            if (apartment is null)
            {
                return new ErrorResult(Messages.ApartmentNotFound);
            }

            apartment.IsActive = false;
            apartment.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            apartment.Udate = DateTime.Now;
            _apartmentDal.Update(apartment);

            return new SuccessResult(Messages.ApartmentUpdated);
        }
    }
}
