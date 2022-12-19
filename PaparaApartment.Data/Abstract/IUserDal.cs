using System;
using System.Collections.Generic;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Dtos.User;
using PaparaApartment.Core.DataAccess;

namespace PaparaApartment.Data.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UserViewDto> GetUserList();
        List<UserClaimsViewDto> GetClaims(int userId);
        int GetUserId(string eMail);
        
    }
}
