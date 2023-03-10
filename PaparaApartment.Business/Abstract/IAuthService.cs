using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Core.Utilities.Security;
using PaparaApartment.Entity.Dtos.User;

namespace PaparaApartment.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserViewDto> Login(UserForLoginDto userForLogin);

        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
