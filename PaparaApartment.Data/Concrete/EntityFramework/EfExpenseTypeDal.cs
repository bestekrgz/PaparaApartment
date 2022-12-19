
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Data.Context;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Data.Abstract;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfExpenseTypeDal : EfEntityRepositoryBase<ExpenseType, PaparaApartmentDbContext>, IExpenseTypeDal
    {
    }
}
