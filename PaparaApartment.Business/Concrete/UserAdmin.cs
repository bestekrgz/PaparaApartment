using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Core.Entities.Concrete;
using PaparaApartment.Entities.Dtos.Car;
using PaparaApartment.Entities.Dtos.User;
using PaparaApartment.Entities.Dtos.UserDetail;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.Apartment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        private IUserDetailService _userDetailManager;
        private IApartmentService _apartmentManager;
        private ICarService _carManager;
        private IUserClaimService _userClaimManager;

        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public UserManager(IUserDal userDal, IMapper mapper, IUserDetailService userDetailManager, IHttpContextAccessor httpContextAccessor, IApartmentService apartmentManager, ICarService carManager, IUserClaimService userClaimManager)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userDetailManager = userDetailManager;
            _httpContextAccessor = httpContextAccessor;
            _apartmentManager = apartmentManager;
            _carManager = carManager;
            _userClaimManager = userClaimManager;
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

        [TransactionScopeAspect]
        public IResult AddWithDetails(UserAddWithDetailsDto newUserWithDetails)
        {
            //eklenecek kullanicini maili check ediliyor
            var result = _userDal.Any(x => x.Email == newUserWithDetails.Email);

            //mail varsa
            if (result)
            {
                //hata mesaji veriliyor
                return new ErrorResult(Messages.UserAlreadyExist);
            }

            //yeni kullanici icin parola olusturuluyor
            var password = PasswordHelper.CreatePassword();

            //bu parolaya ait hash ve salt degerleri olusturuluyor
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            //yeni bir kullanici instance olusturuluyor ve alinan biligilerden User tablosu ile ilgili olanlar map ediliyor
            var newUser = _mapper.Map<User>(newUserWithDetails);

            //User tablosunda tutulacak olan icin psswordHash ve passwordSalt degerleri nesneye yaziliyor
            newUser.PasswordSalt = passwordSalt;
            newUser.PasswordHash = passwordHash;

            //yeni kullanici user tablosuna ekleniyor
            var isUserAdd = Add(newUser);

            //ekleme isleminde problem olursa hata bildiriliyor
            if (!isUserAdd.Success)
            {
                return new ErrorResult(Messages.UserAddFailed);
            }

            //iliskili tablolarda userId gerekli oldugu icin yeni kullanicinin id bilgisi aliniyor
            var newUserId = GetUserId(newUserWithDetails.Email);

            //linan bilgilerden userDetail tablosu ile ilgili olanlar olusturulan UserDetailAdd instance na map ediliyor
            var userDetail = _mapper.Map<UserDetailAddDto>(newUserWithDetails);

            //userDetail icin gerekli olan baglantili tablo Id (UserId) bilgisi yaziliyor
            userDetail.Id = newUserId;

            //userDetail tablosuna yeni kayit ekleniyor
            var isUserDetailAdd = _userDetailManager.Add(userDetail);

            //ekleme isleminde problem olursa hata bildiriliyor
            if (!isUserDetailAdd.Success)
            {
                return new ErrorResult(Messages.UserDetailAddFailed);
            }


            //apartment tablosunun update icin bilgileri alinarak yeni bir update instance olusturuluyor
            var updateApartmentUser = _mapper.Map<ApartmentUserUpdateDto>(newUserWithDetails);

            //kullanici idsi ekleniyor
            updateApartmentUser.UserId = newUserId;

            //apartment tablosundaki kullanici bilgileri degistiriliyor
            _apartmentManager.UpdateUser(updateApartmentUser);

            //yeni eklenen kullanici icin default yetki atamasi yapiliyor
            _userClaimManager.AddDefault(newUserId);

            //arac plaka bilgisi varsa
            if (newUserWithDetails.LicensePlate != null)
            {
                foreach (string licensePlate in newUserWithDetails.LicensePlate.ToArray())
                {
                    //herbir plaka icin car tablosuna kayit ekleniyor
                    _carManager.Add(new CarAddDto() { LicensePlate = licensePlate, UserId = newUserId });
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
