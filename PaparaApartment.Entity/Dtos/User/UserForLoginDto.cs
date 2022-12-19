
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.User
{
    public class UserForLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
