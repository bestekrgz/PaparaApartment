using AutoMapper;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Business.Abstract;
using PaparaApartment.Business.Aspects;
using PaparaApartment.Business.Constant;
using PaparaApartment.Core.Aspects;
using PaparaApartment.Core.Extensions;
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Data.Abstract;
using PaparaApartment.Entity.Dtos.Message;
using PaparaApartment.Entity.Dtos.UserMessage;
using PaparaApartment.Entity.Concrete;
using System;


namespace PaparaApartment.Business.Concrete
{
    public class MessageAdmin : IMessageService
    {
        private IMessageDal _messageDal;
        private IUserService _userAdmin;
        private IApartmentService _apartmentAdmin;
        private IUserMessageService _userMessageAdmin;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public MessageAdmin(IMessageDal messageDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userAdmin, IUserMessageService userMessageAdmin, IApartmentService apartmentAdmin)
        {
            _messageDal = messageDal;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userAdmin = userAdmin;
            _userMessageAdmin = userMessageAdmin;
            _apartmentAdmin = apartmentAdmin;
        }

        public void Add(MessageAddDto messageAddDto)
        {
            var newMessage = _mapper.Map<Message>(messageAddDto);

            newMessage.IuserId = _httpContextAccessor.HttpContext.User.GetLoggedUserId();
            newMessage.Idate = DateTime.Now;

            _messageDal.Add(newMessage);
        }

        [SecuredOperation("admin")]
        [TransactionScopeAscpect]
        public IResult SendMessageToAll(MessageAddDto messageAddForAllDto)
        {
            Add(messageAddForAllDto);
            var messageId = GetLastMessageId();
            var userList = _userAdmin.GetAll();
            foreach (var user in userList.Data.ToArray())
            {
                _userMessageAdmin.Add(new UserMessageAddDto()
                {
                    MessageId = messageId,
                    ToUserId = user.Id,
                });
            }

            return new SuccessResult(Messages.MessageSendAll);
        }

        [TransactionScopeAscpect]
        public IResult SendMessageToOne(MessageAddForOneDto messageAddForOneDto)
        {
            if (!_userAdmin.UserExistsId(userId: messageAddForOneDto.RecipientId))
            {
                return new ErrorResult(Messages.RecipientNotFound);
            }

            var newMessage = _mapper.Map<MessageAddDto>(messageAddForOneDto);

            Add(newMessage);

             var messageId = GetLastMessageId();

            _userMessageAdmin.Add(new UserMessageAddDto()
            {
                MessageId = messageId,
                ToUserId = messageAddForOneDto.RecipientId,
            });

            return new SuccessResult(Messages.MessageSend);
        }

        public int GetLastMessageId()
        {
            return _messageDal.GetLastMessageId();
        }

        public IDataResult<MessageViewDto> GetMessageById(int messageId)
        {
            var message = _messageDal.Get(x => x.Id == messageId);
            if (message is null)
            {
                return new ErrorDataResult<MessageViewDto>(Messages.MessageNotFound);
            }
            var viewMessage = _mapper.Map<MessageViewDto>(message);
            return new SuccessDataResult<MessageViewDto>(viewMessage);
        }
    }
}
