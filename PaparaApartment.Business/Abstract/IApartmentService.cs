using System.Collections.Generic;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.Apartment;
using PaparaApartment.Entity.Dtos.User;

namespace PaparaApartment.Business.Abstract
{
    public interface IApartmentService
    {
        IResult Add(ApartmentAddDto apartmentAddDto);
        IResult Update(ApartmentUpdateDto apartmentUpdateDto);
        IResult UpdateUser(ApartmentUserUpdateDto apartmentUpdateDto);

        IResult UpdateStatus(int apartmentId,bool status);
        IResult Delete(int apartmentId);
        IDataResult<List<ApartmentViewDto>> GetAll();

        IDataResult<List<UserViewDto>> GetAllResident();
        List<int> GetIdList();
        int GetIdByResidentId(int residentId);
        bool IsHirer(int residentId);
    }
}
