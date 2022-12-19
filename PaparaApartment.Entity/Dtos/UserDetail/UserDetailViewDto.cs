
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.UserDetail
{
    public class UserDetailViewDto:IDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNo { get; set; }
    }
}
