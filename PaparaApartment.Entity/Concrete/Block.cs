using PaparaApartment.Core.Entities;
using System;


namespace PaparaApartment.Entity.Concrete
{
    public partial class Block : IEntity
    {
        public short Id { get; set; }
        public string Letter { get; set; }
        public bool IsActive { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
