using PaparaApartment.Core.DataAccess;
using PaparaApartment.Entity.Concrete;
using PaparaApartment.Entity.Dtos.Message;

namespace PaparaApartment.Data.Abstract
{
    public interface IMessageDal : IEntityRepository<Message>
    {
        int GetLastMessageId();
    }
}
