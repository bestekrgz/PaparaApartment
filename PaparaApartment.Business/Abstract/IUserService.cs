
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.User;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<UserViewDto>> GetAll();
        User GetByMail(string mail);
        User GetUserById(int userId);

        bool UserExistsId(int userId);

        bool UserExistsMail(string mail);

        int GetUserId(string mail);

        List<UserClaimsViewDto> GetClaims(int userId);
        IResult Add(User newUser);

        IResult AddWithDetails(UserAddWithDetailsDto newUserWithDetails);

        IResult Delete(int userId);

        IResult Update(UserUpdateDto userUpdateInfo);

        IResult PasswordReset(int userId);
    }
}
