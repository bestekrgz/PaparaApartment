using AutoMapper;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Dtos.User;
using System.Collections.Generic;
using PaparaApartment.Core.Utilities.Security;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Utilities.Security.Hashing;

namespace PaparaApartment.Business.Concrete
{
    public class AuthAdmin : IAuthService
    {
        private IUserService _userAdmin;
        private ITokenHelper _tokenHelper;
        private IMapper _mapper;

        public AuthAdmin(IUserService userAdmin, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userAdmin = userAdmin;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public IDataResult<UserViewDto> Login(UserForLoginDto userForLogin)
        {
            var userToCheck = _userAdmin.GetByMail(userForLogin.Email);

            if (userToCheck is null)
            {
                return new ErrorDataResult<UserViewDto>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLogin.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<UserViewDto>(Messages.PasswordError);
            }

            return new SuccessDataResult<UserViewDto>(_mapper.Map<UserViewDto>(userToCheck),
                Messages.UserLoginSuccessful);

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var userClaims = _userAdmin.GetClaims(user.Id);

            var accessToken = _tokenHelper.CreateToken(user, _mapper.Map<List<Claim>>(userClaims));

            return new SuccessDataResult<AccessToken>(accessToken);
        }
    }
}
