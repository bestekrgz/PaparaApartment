using PaparaApartment.Core.Entities;
using System;

namespace PaparaApartment.Entity.Concrete
{
    public partial class ApartmentExpense : IEntity
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
        public bool DidPay { get; set; }
        public bool IsActive { get; set; }
        public int IuserId { get; set; }
        public DateTime Idate { get; set; }
        public int? UuserId { get; set; }
        public DateTime? Udate { get; set; }
    }
}
