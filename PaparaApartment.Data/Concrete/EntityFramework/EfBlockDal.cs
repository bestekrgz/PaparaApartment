using PaparaApartment.Data.Abstract;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Data.Context;
using PaparaApartment.Core.Data.EntitiyFramework;

namespace PaparaApartment.Data.Concrete.EntityFramework
{
    public class EfBlockDal : EfEntityRepositoryBase<Block, PaparaApartmentDbContext>, IBlockDal
    {
    }
}
