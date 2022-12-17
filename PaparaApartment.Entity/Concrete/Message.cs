using PaparaApartment.Core.Entities;
using System;

namespace PaparaApartment.Entity.Concrete
{
    public partial class Message : IEntity
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
