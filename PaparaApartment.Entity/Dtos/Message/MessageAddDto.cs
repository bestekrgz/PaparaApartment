
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.Message
{
    public class MessageAddDto:IDto
    {
        public string Subject { get; set; }
        public string MessageText { get; set; }
    }
}
