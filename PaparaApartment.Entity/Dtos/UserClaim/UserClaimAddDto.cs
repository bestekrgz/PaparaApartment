
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.UserClaim
{
    public class UserClaimAddDto:IDto
    {
        public int UserId { get; set; }
        public short ClaimId { get; set; }
    }
}
