using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Business.Abstract
{
    public interface IMessageService
    {
        IResult SendMessageToAll(MessageAddDto messageAddForAllDto);
        IResult SendMessageToOne(MessageAddForOneDto messageAddForOneDto);
        void Add(MessageAddDto messageAddDto);
        int GetLastMessageId();
        IDataResult<MessageViewDto> GetMessageById(int messageId);
    }
}
