using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using System.Collections.Generic;
using PaparaApartment.Entity.Dtos.Apartment;
using PaparaApartment.Entity.Dtos.User;

namespace PaparaApartment.Data.Abstract
{
    public interface IApartmentDal : IEntityRepository<Apartment>
    {
        List<ApartmentViewDto> GetListWithDetails();
        List<int> GetApartmentIdList();

        List<UserViewDto> GetResidentList();
    }
}
