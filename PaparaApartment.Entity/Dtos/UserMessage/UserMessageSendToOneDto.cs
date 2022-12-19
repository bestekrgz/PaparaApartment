using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserMessage
{
    public class UserMessageSendToOneDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public int RecipientId { get; set; }
    }
}