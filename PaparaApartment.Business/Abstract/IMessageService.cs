

using PaparaApartment.Core.Utilities.Result;
using PaparaApartment.Entity.Dtos.Message;

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
