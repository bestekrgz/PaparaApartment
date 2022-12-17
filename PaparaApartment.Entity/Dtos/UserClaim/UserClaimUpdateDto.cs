
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.UserClaim
{
    public class UserClaimUpdateDto:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public short ClaimId { get; set; }
    }
}
