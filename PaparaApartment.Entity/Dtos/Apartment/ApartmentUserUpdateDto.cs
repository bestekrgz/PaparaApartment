
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.Apartment
{
    public class ApartmentUserUpdateDto:IDto
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public bool IsHirer { get; set; }
        public bool IsResident { get; set; }
    }
}
