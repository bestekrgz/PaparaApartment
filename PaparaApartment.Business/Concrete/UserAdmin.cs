using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entity.Dtos.Car;
using PaparaApartment.Entity.Dtos.User;
using PaparaApartment.Entity.Dtos.UserDetail;
using PaparaApartment.Entity.Dtos.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Business.Aspects;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Aspects;
using PaparaApartment.Business.Constant;
using Configuration.Core.Utilities.Security.PasswordCreator;
using PaparaApartment.Core.Utilities.Security.Hashing;

namespace PaparaApartment.Business.Concrete
{
    public class UserAdmin : IUserService
    {
        private IUserDal _userDal;
        private IUserDetailService _userDetailAdmin;
        private IApartmentService _apartmentAdmin;
        private ICarService _carAdmin;
        private IUserClaimService _userClaimAdmin;

        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public UserAdmin(IUserDal userDal, IMapper mapper, IUserDetailService userDetailAdmin, IHttpContextAccessor httpContextAccessor, IApartmentService apartmentAdmin, ICarService carAdmin, IUserClaimService userClaimAdmin)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userDetailAdmin = userDetailAdmin;
            _httpContextAccessor = httpContextAccessor;
            _apartmentAdmin = apartmentAdmin;
            _carAdmin = carAdmin;
            _userClaimAdmin = userClaimAdmin;
        }

        [SecuredOperation("admin")]
        public IDataResult<List<UserViewDto>> GetAll()
        {
            var userList = _userDal.GetUserList();

            return new SuccessDataResult<List<UserViewDto>>(userList);
        }

        public IResult Add(User newUser)
        {
            newUser.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newUser.Idate = DateTime.Now;
            _userDal.Add(newUser);
            return new SuccessResult();
        }

        [TransactionScopeAscpect]
        public IResult AddWithDetails(UserAddWithDetailsDto newUserWithDetails)
        {
            var result = _userDal.Any(x => x.Email == newUserWithDetails.Email);

            if (result)
            {
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            var password = PasswordHelper.CreatePassword();

            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            var newUser = _mapper.Map<User>(newUserWithDetails);

            newUser.PasswordSalt = passwordSalt;
            newUser.PasswordHash = passwordHash;

            var isUserAdd = Add(newUser);

            if (!isUserAdd.Success)
            {
                return new ErrorResult(Messages.UserAddFailed);
            }

            var newUserId = GetUserId(newUserWithDetails.Email);

            var userDetail = _mapper.Map<UserDetailAddDto>(newUserWithDetails);

            userDetail.Id = newUserId;

            var isUserDetailAdd = _userDetailAdmin.Add(userDetail);

            if (!isUserDetailAdd.Success)
            {
                return new ErrorResult(Messages.UserDetailAddFailed);
            }


            var updateApartmentUser = _mapper.Map<ApartmentUserUpdateDto>(newUserWithDetails);

            updateApartmentUser.UserId = newUserId;

            _apartmentAdmin.UpdateUser(updateApartmentUser);

            _userClaimAdmin.AddDefault(newUserId);

            if (newUserWithDetails.LicensePlate != null)
            {
                foreach (string licensePlate in newUserWithDetails.LicensePlate.ToArray())
                {
                    _carAdmin.Add(new CarAddDto() { LicensePlate = licensePlate, UserId = newUserId });
                }
            }

            return new SuccessResult(Messages.UserAddedWithInfos);
        }

        public IResult Delete(int userId)
        {
            var user = _userDal.Get(x => x.Id == userId);

            if (user is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            user.IsActive = false;
            user.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            user.Udate = DateTime.Now;
            _userDal.Update(user);

            return new SuccessResult(Messages.UserRemoved);
        }

        public IResult Update(UserUpdateDto userUpdateInfo)
        {
            var updateUser = GetUserById(userUpdateInfo.Id);
            updateUser = _mapper.Map(userUpdateInfo, updateUser);
            updateUser.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            updateUser.Udate = DateTime.Now;
            _userDal.Update(updateUser);

            return new SuccessResult(Messages.UserUpdated);
        }

        public User GetByMail(string mail)
        {
            var user = _userDal.Get(x => x.Email == mail);
            return user;
        }

        public User GetUserById(int userId)
        {
            var user = _userDal.Get(x => x.Id == userId);
            return user;
        }

        public bool UserExistsId(int userId)
        {
            var result = _userDal.Any(x => x.Id == userId);
            return result;
        }

        public bool UserExistsMail(string mail)
        {
            var result = _userDal.Any(x => x.Email == mail);
            return result;
        }

        public int GetUserId(string mail)
        {
            return _userDal.GetUserId(mail);
        }

        public List<UserClaimsViewDto> GetClaims(int userId)
        {
            var userClaims = _userDal.GetClaims(userId);

            return userClaims;
        }

        public IResult PasswordReset(int userId)
        {
            var userToCheck = GetUserById(userId);

            if (userToCheck is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var newPassword = PasswordHelper.CreatePassword();

            HashingHelper.CreatePasswordHash(newPassword, out var passwordHash, out var passwordSalt);

            userToCheck.PasswordSalt = passwordSalt;
            userToCheck.PasswordHash = passwordHash;
            Update(_mapper.Map<UserUpdateDto>(userToCheck));

            return new SuccessResult(Messages.UserPasswordReset);
        }

    }
}
