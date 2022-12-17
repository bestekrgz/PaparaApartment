using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaApartment.Core.Entities.Concrete
{
    public class UserClaim : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public short ClaimId { get; set; }
        public bool IsActive { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
