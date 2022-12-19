
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserMessage
{
    public class UserMessageSendToAllDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
