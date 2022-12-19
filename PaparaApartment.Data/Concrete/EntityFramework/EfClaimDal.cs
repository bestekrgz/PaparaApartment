using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Data.Context;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Core.Data.EntitiyFramework;
using System.Security.Claims;

namespace PaparaApartment.Data.Concrete.EntityFramework
{
    public class EfClaimDal : EfEntityRepositoryBase<Claim, PaparaApartmentDbContext>, IClaimDal
    {
    }
}
