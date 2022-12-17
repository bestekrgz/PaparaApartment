
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Car
{
    public class CarAddDto:IDto
    {
        public int UserId { get; set; }
        public string LicensePlate { get; set; }
    }
}
