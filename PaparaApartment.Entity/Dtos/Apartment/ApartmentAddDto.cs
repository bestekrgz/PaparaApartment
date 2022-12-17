
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.Apartment
{
    public class ApartmentAddDto:IDto
    {
        public short BlockId { get; set; }
        public int Floor { get; set; }
        public short DoorNumber { get; set; }
        public bool Status { get; set; }
        public string Type { get; set; }
    }
}
