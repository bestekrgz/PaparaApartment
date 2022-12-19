using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entities.Dtos.UserMessage;
using PaparaApartment.Entity.Concrete;
using System;
using System.Collections.Generic;


namespace PaparaApartment.Business.Concrete
{
    public class UserMessageManager : IUserMessageService
    {
        private IUserMessageDal _userMessageDal;
        private IUserService _userManager;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        public UserMessageManager(IUserMessageDal userMessageDal, IMapper mapper, IUserService userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userMessageDal = userMessageDal;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public void Add(UserMessageAddDto userMessageAddDto)
        {
            var newUserMessage = _mapper.Map<UserMessage>(userMessageAddDto);

            newUserMessage.FromUserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newUserMessage.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newUserMessage.Idate = DateTime.Now;
            newUserMessage.IsNew = true;
            newUserMessage.IsRead = false;
            newUserMessage.IsActiveFuser = true;
            newUserMessage.IsActiveToUser = true;
            _userMessageDal.Add(newUserMessage);
        }

        public IResult Delete(int userMessageId, bool isSender)
        {
            var userMessage = _userMessageDal.Get(x => x.Id == userMessageId);
            if (userMessage is null)
            {
                return new ErrorResult(Messages.UserMessageNotFound);
            }
            userMessage.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            userMessage.Udate = DateTime.Now;
            if (isSender)
            {
                userMessage.IsActiveFuser = false;
            }
            else
            {
                userMessage.IsActiveToUser = false;
            }
            _userMessageDal.Update(userMessage);
            return new SuccessResult(Messages.UserMessageRemoved);
        }

        public IDataResult<List<UserMessageIncomingViewDto>> GetUserIncomingMessages()
        {
            var incomingMessages = _userMessageDal.GetIncomingMessages(_httpContextAccessor.HttpContext.User.GetLoggedUserId());
            if (incomingMessages is null)
            {
                return new ErrorDataResult<List<UserMessageIncomingViewDto>>(Messages.UserMessageIncomingNotExist);
            }

            return new SuccessDataResult<List<UserMessageIncomingViewDto>>(incomingMessages);
        }

        public IDataResult<List<UserMessageSentViewDto>> GetUserSentMessages()
        {
            var sentMessages = _userMessageDal.GetSentMessages(_httpContextAccessor.HttpContext.User.GetLoggedUserId());
            if (sentMessages is null)
            {
                return new ErrorDataResult<List<UserMessageSentViewDto>>(Messages.UserMessageSentNotExist);
            }

            return new SuccessDataResult<List<UserMessageSentViewDto>>(sentMessages);
        }

        public IResult UpdateReadStatus(int userMessageId, bool status, bool newStatus = false)
        {
            var userMessage = _userMessageDal.Get(x => x.Id == userMessageId);
            if (userMessage is null)
            {
                return new ErrorResult(Messages.UserMessageNotFound);
            }

            userMessage.IsRead = status;
            userMessage.IsNew = newStatus;
            userMessage.UuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            userMessage.Udate = DateTime.Now;
            _userMessageDal.Update(userMessage);
            return new SuccessResult(Messages.UserMessageUpdated);
        }
    }
}
