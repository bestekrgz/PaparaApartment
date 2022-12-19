
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.UserClaim;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{
    public interface IUserClaimService
    {
        IDataResult<List<UserClaimListViewDto>> GetUserClaimList();
        IResult Add(UserClaimAddDto userClaimAddDto);

        IResult AddDefault(int userId, short claimId = 2);

        IResult Update(UserClaimUpdateDto userClaimUpdateDto);
        IResult Delete(int userClaimId);
    }
}
