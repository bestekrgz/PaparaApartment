
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.Message
{
    public class MessageAddForOneDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public int RecipientId { get; set; }
    }
}
