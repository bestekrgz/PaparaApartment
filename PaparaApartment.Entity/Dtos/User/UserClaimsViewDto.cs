using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.User
{
    public class UserClaimsViewDto:IDto
    {
        public short Id { get; set; }
        public string ClaimName { get; set; }
    }
}