using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
