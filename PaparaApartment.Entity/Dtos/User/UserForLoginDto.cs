
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.User
{
    public class UserForLoginDto:IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
