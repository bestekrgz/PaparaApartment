using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entity.Dtos.UserClaim;
using System;
using System.Collections.Generic;
u

namespace PaparaApartment.Business.Concrete
{
    public class UserClaimAdmin : IUserClaimService
    {
        private IUserClaimDal _userClaimDal;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public UserClaimAdmin(IUserClaimDal userClaimDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userClaimDal = userClaimDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public IDataResult<List<UserClaimListViewDto>> GetUserClaimList()
        {
            var userClaimList = _userClaimDal.GetUserClaimListWithDetails();
            if (userClaimList is null)
            {
                return new ErrorDataResult<List<UserClaimListViewDto>>(Messages.UserClaimListNoxExist);
            }
            return new SuccessDataResult<List<UserClaimListViewDto>>(userClaimList);
        }

        public IResult Add(UserClaimAddDto userClaimAddDto)
        {
            var check = _userClaimDal.Any(x => x.UserId == userClaimAddDto.UserId && x.ClaimId == userClaimAddDto.ClaimId);
            if (check)
            {
                return new ErrorResult(Messages.UserClaimAlreadyExist);
            }

            var newUserClaim = _mapper.Map<UserClaim>(userClaimAddDto);
            newUserClaim.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newUserClaim.Idate = DateTime.Now;
            _userClaimDal.Add(newUserClaim);
            return new SuccessResult(Messages.UserClaimAdded);
        }

        public IResult AddDefault(int userId, short claimId = 2)
        {
            _userClaimDal.Add(new UserClaim()
            {
                UserId = userId,
                ClaimId = claimId,
                IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId(),
                Idate = DateTime.Now
            });
            return new SuccessResult(Messages.UserClaimAdded);
        }

        public IResult Update(UserClaimUpdateDto userClaimUpdateDto)
        {
            var userClaim = (_userClaimDal).Get(x => x.Id == userClaimUpdateDto.Id);
            if (userClaim is null)
            {
                return new ErrorResult(Messages.UserClaimNotFound);
            }

            userClaim = _mapper.Map(userClaimUpdateDto, userClaim);
            userClaim.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            userClaim.Udate = DateTime.Now;
            _userClaimDal.Add(userClaim);
            return new SuccessResult(Messages.UserClaimUpdated);
        }

        public IResult Delete(int userClaimId)
        {
            var userClaim = _userClaimDal.Get(x => x.Id == userClaimId);
            if (userClaim is null)
            {
                return new ErrorResult(Messages.UserClaimNotFound);
            }

            var claimCount = _userClaimDal.GetList(x => x.UserId == userClaim.UserId).Count;
            if (claimCount <= 1)
            {
                return new ErrorResult(Messages.UserClaimCanNotBeRemoved);
            }

            userClaim.IsActive = false;
            userClaim.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            userClaim.Udate = DateTime.Now;
            _userClaimDal.Update(userClaim);
            return new SuccessResult(Messages.CarRemoved);
        }
    }
}
