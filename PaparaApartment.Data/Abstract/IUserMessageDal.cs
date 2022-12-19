using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using System.Collections.Generic;
using PaparaApartment.Entity.Dtos.UserMessage;

namespace PaparaApartment.Data.Abstract
{
    public interface IUserMessageDal : IEntityRepository<UserMessage>
    {
        List<UserMessageIncomingViewDto> GetIncomingMessages(int userId);
        List<UserMessageSentViewDto> GetSentMessages(int userId);
    }
}

