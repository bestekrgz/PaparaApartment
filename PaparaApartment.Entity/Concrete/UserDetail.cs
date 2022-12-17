using PaparaApartment.Core.Entities;
using System;


namespace PaparaApartment.Entity.Concrete
{
    public partial class UserDetail : IEntity
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNo { get; set; }
        public bool IsActive { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
