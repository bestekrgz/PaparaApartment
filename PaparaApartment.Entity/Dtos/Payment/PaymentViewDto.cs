
using PaparaApartment.Core.Entities;
using System;

namespace PaparaApartment.Entities.Dtos.Payment
{
    public class PaymentViewDto:IDto
    {
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
        public string CardName { get; set; }
        public string Type { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
