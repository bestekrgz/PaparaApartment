using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Business.Abstract
{
    internal class IClaimService
    {
        public interface IClaimService
        {
            IDataResult<List<ClaimViewDto>> GetAll();

            IResult Add(ClaimAddDto claimAddDto);

            IResult Update(ClaimUpdateDto claimUpdateDto);
            IResult Delete(int claimId);

        }
    }
}
