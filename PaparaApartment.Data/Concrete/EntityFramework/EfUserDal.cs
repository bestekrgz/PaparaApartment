
using System.Collections.Generic;
using System.Linq;
using PaparaApartment.Data.Context;
using PaparaApartment.Core.Data.EntitiyFramework;
using PaparaApartment.Entity.Dtos.User;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Core.Entities.Concrete;

namespace ApartmentManagement.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, PaparaApartmentDbContext>, IUserDal
    {
        public List<UserViewDto> GetUserList()
        {
            using (var context = new PaparaApartmentDbContext())
            {
                var result = (from user in context.Users
                              join apartment in context.Apartments
                                  on user.Id equals apartment.OwnerId
                              join userDetail in context.UserDetails
                                  on user.Id equals userDetail.Id
                              join block in context.Blocks
                                  on apartment.BlockId equals block.Id
                              where user.IsActive == true
                              select new UserViewDto()
                              {
                                  Id = user.Id,
                                  Name = user.FirstName + " " + user.LastName,
                                  PhoneNumber = userDetail.PhoneNumber,
                                  Email = user.Email,
                                  Block = block.Letter.ToUpper(),
                                  DoorNumber = apartment.DoorNumber,
                                  Title = "Owner"
                              }).Union(from user in context.Users
                                       join apartment in context.Apartments
                                           on user.Id equals apartment.HirerId
                                       join userDetail in context.UserDetails
                                           on user.Id equals userDetail.Id
                                       join block in context.Blocks
                                           on apartment.BlockId equals block.Id
                                       where user.IsActive == true
                                       select new UserViewDto()
                                       {
                                           Id = user.Id,
                                           Name = user.FirstName + " " + user.LastName,
                                           PhoneNumber = userDetail.PhoneNumber,
                                           Email = user.Email,
                                           Block = block.Letter.ToUpper(),
                                           DoorNumber = apartment.DoorNumber,
                                           Title = "Hirer"
                                       }).OrderBy(x => x.Block).ThenBy(x => x.DoorNumber);
                return result.ToList();
            }
        }
        

    public List<UserClaimsViewDto> GetClaims(int userId)
    {
        using (var context = new PaparaApartmentDbContext())
        {
            var result = from userClaim in context.UserClaims
                         join claim in context.Claims
                             on userClaim.ClaimId equals claim.Id
                         where userClaim.Id == userId && claim.IsActive == true
                         select new UserClaimsViewDto()
                         {
                             Id = claim.Id,
                             ClaimName = claim.Name
                         };
            return result.ToList();
        }
    }

    public int GetUserId(string eMail)
    {
        using (var context = new PaparaApartmentDbContext())
        {
            return context.Set<User>().SingleOrDefault(x => x.Email == eMail).Id;
        }
    }


}
}
