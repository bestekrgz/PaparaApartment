
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entities.Dtos.Claim;
using System.Collections.Generic;


namespace PaparaApartment.Business.Abstract
{


        public interface IClaimService
        {
            IDataResult<List<ClaimViewDto>> GetAll();

            IResult Add(ClaimAddDto claimAddDto);

            IResult Update(ClaimUpdateDto claimUpdateDto);
            IResult Delete(int claimId);

        }
 
}
