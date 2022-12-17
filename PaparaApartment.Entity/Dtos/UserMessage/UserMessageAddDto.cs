
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.UserMessage
{
    public class UserMessageAddDto:IDto
    {
        public int MessageId{ get; set; }
        public int ToUserId { get; set; }
    }
}
