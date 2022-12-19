using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entities.Dtos.Claim;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;
using Claim = PaparaApartment.Core.Entities.Concrete.Claim;

namespace PaparaApartment.Business.Concrete
{
    public class ClaimAdmin: IClaimService
    {
        private IClaimDal _claimDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public ClaimAdmin(IClaimDal claimDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _claimDal = claimDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDataResult<List<ClaimViewDto>> GetAll()
        {
            var claimList = _claimDal.GetList();
            if (claimList is null)
            {
                return new ErrorDataResult<List<ClaimViewDto>>(Messages.ClaimListNoxExist);
            }

            var claımViewList = _mapper.Map<List<ClaimViewDto>>(claimList);
            return new SuccessDataResult<List<ClaimViewDto>>(claımViewList);
        }

        public IResult Add(ClaimAddDto claimAddDto)
        {
            var claimCheck = _claimDal.Any(x => x.Name == claimAddDto.Name);
            if (claimCheck)
            {
                return new ErrorResult(Messages.ClaimAlreadyExist);
            }

            var newClaim = _mapper.Map<Claim>(claimAddDto);
            newClaim.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newClaim.Idate = DateTime.Now;
            _claimDal.Add(newClaim);
            return new SuccessResult(Messages.ClaimAdded);
        }

        public IResult Update(ClaimUpdateDto claimUpdateDto)
        {
            var claim = _claimDal.Get(x => x.Id == claimUpdateDto.Id);
            if (claim is null)
            {
                return new ErrorResult(Messages.ClaimNotFound);
            }

            claim = _mapper.Map(claimUpdateDto, claim);
            claim.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            claim.Udate = DateTime.Now;
            _claimDal.Add(claim);
            return new SuccessResult(Messages.ClaimUpdated);
        }

        public IResult Delete(int claimId)
        {
            var claim = _claimDal.Get(x => x.Id == claimId);
            if (claim is null)
            {
                return new ErrorResult(Messages.ClaimNotFound);
            }

            claim.IsActive = false;
            claim.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            claim.Udate = DateTime.Now;
            _claimDal.Update(claim);
            return new SuccessResult(Messages.ClaimRemoved);
        }
    }
}
