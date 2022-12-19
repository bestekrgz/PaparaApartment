using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.UserDetail;

namespace PaparaApartment.Data.Abstract
{
    public interface IUserDetailDal : IEntityRepository<UserDetail>
    {
        UserDetailViewDto GetForView(int userId);
    }
}
