
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserClaim
{
    public class UserClaimListViewDto:IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public short ClaimId { get; set; }
        public string ClaimName { get; set; }
    }
}
