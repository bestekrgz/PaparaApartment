
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Message
{
    public class MessageAddDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
