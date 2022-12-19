
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserClaim
{
    public class UserClaimAddDto:IDto
    {
        public int UserId { get; set; }
        public short ClaimId { get; set; }
    }
}
