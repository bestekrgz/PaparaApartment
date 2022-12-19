
using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.UserMessage;
using System.Collections.Generic;

namespace PaparaApartment.Business.Abstract
{
    public interface IUserMessageService
    {

        void Add(UserMessageAddDto userMessageAddDto);
        IResult Delete(int userMessageId, bool isSender);

        IDataResult<List<UserMessageIncomingViewDto>> GetUserIncomingMessages();
        IDataResult<List<UserMessageSentViewDto>> GetUserSentMessages();

        IResult UpdateReadStatus(int userMessageId, bool status, bool newStatus = false);
    }
}
