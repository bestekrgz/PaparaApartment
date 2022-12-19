using System.Linq;
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Data.Context;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.UserDetail;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfUserDetailDal : EfEntityRepositoryBase<UserDetail, PaparaApartmentDbContext>, IUserDetailDal
    {
        public UserDetailViewDto GetForView(int userId)
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = from userDetail in context.UserDetails
                             join user in context.Users
                                 on userDetail.Id equals user.Id
                             where userDetail.Id == userId
                             select new UserDetailViewDto()
                             {
                                 Name = user.FirstName + " " + user.LastName,
                                 PhoneNumber = userDetail.PhoneNumber,
                                 IdentityNo = userDetail.IdentityNo
                             };
                return result.Single();
            }
        }
    }
}
