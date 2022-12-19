using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Entity.Dtos.Car;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;

namespace PaparaApartment.Business.Concrete
{
    public class CarAdmin : ICarService
    {
        private ICarDal _carDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public CarAdmin(ICarDal carDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _carDal = carDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }


        public IResult Add(CarAddDto carAddDto)
        {
            var carCheck = _carDal.Any(x => x.LicensePlate == carAddDto.LicensePlate);
            if (carCheck)
            {
                return new ErrorResult(Messages.CarAlreadyExist);
            }

            var newCar = _mapper.Map<Car>(carAddDto);
            newCar.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newCar.Idate = DateTime.Now;
            newCar.IsActive = true;
            _carDal.Add(newCar);
            return new SuccessResult(Messages.CarAdded);
        }

        public IDataResult<List<CarViewDto>> GetAll()
        {
            var carList = _carDal.GetCarListWithDetails();
            if (carList is null)
            {
                return new ErrorDataResult<List<CarViewDto>>(Messages.CarListNoxExist);
            }
            return new SuccessDataResult<List<CarViewDto>>(carList);
        }

        public IDataResult<List<CarViewDto>> GetByUserId(int userId)
        {
            var carList = _carDal.GetCarListWithDetails(x => x.UserId == userId);
            if (carList is null)
            {
                return new ErrorDataResult<List<CarViewDto>>(Messages.UserCarNotFound);
            }
            return new SuccessDataResult<List<CarViewDto>>(carList);
        }

        public IResult Update(CarUpdateDto carUpdateDto)
        {
            var car = _carDal.Get(x => x.LicensePlate == carUpdateDto.LicensePlate);
            if (car is null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }

            car = _mapper.Map(carUpdateDto, car);
            car.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            car.Udate = DateTime.Now;
            _carDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult Delete(int carId)
        {
            var car = _carDal.Get(x => x.Id == carId);
            if (car is null)
            {
                return new ErrorResult(Messages.CarNotFound);
            }

            car.IsActive = false;
            car.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            car.Udate = DateTime.Now;
            _carDal.Update(car);
            return new SuccessResult(Messages.CarRemoved);
        }
    }
}
