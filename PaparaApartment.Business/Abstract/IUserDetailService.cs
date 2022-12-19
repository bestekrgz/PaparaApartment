

using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.UserDetail;

namespace PaparaApartment.Business.Abstract
{
    public interface IUserDetailService
    {
        IDataResult<UserDetailViewDto> GetById(int userId);

        IResult Add(UserDetailAddDto userDetailAdd);
        IResult Delete(int userId);

        IResult Update(UserDetailUpdateDto userDetailUpdate);
    }
}
