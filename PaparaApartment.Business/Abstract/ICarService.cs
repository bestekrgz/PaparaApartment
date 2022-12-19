
using System.Collections.Generic;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.Car;

namespace PaparaApartment.Business.Abstract
{
    public interface ICarService
    {
        IResult Add(CarAddDto carAddDto);

        IDataResult<List<CarViewDto>> GetAll();

        IDataResult<List<CarViewDto>> GetByUserId(int userId);

        IResult Update(CarUpdateDto carUpdateDto);
        IResult Delete(int carId);

    }
}
