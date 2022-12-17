
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.UserMessage
{
    public class UserMessageSendToAllDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
