
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PaparaApartment.Data.Context;
using PaparaApartment.Entity.Dtos.Car;
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entity.Concrete;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, PaparaApartmentDbContext>, ICarDal
    {
        public List<CarViewDto> GetCarListWithDetails(Expression<Func<CarViewDto, bool>> filter = null)
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = from car in context.Cars
                             join user in context.Users
                                 on car.UserId equals user.Id
                             where car.IsActive == true
                             select new CarViewDto()
                             {
                                 Id = car.Id,
                                 UserId = user.Id,
                                 UserName = user.FirstName + " " + user.LastName,
                                 LicensePlate = car.LicensePlate
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
