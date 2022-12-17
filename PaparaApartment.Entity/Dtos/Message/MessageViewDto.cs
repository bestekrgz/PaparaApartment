
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Message
{
    public class MessageViewDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
