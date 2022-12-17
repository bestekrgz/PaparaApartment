
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Car
{
    public class CarViewDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LicensePlate { get; set; }

    }
}
