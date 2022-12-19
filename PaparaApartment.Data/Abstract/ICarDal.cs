using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaparaApartment.Entity.Dtos.Car;

namespace PaparaApartment.Data.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        List<CarViewDto> GetCarListWithDetails(Expression<Func<CarViewDto, bool>> filter = null);
    }
}
