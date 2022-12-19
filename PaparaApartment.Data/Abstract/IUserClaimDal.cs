using PaparaApartment.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Dtos.UserClaim;

namespace PaparaApartment.Data.Abstract
{
    public interface IUserClaimDal : IEntityRepository<UserClaim>
    {
        List<UserClaimListViewDto> GetUserClaimListWithDetails(
            Expression<Func<UserClaimListViewDto, bool>> filter = null);
    }
}
