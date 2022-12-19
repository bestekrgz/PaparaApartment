
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.Car
{
    public class CarUpdateDto:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LicensePlate { get; set; }
    }
}
