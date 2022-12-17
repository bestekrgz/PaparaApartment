using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
