using System.Collections.Generic;
using PaparaApartment.Core.Entities.Concrete;

namespace PaparaApartment.Core.Utilities.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<Claim> userClaims);
    }
}
