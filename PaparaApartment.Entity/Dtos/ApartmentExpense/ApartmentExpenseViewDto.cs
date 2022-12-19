using System;
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.ApartmentExpense
{
    public class ApartmentExpenseViewDto:IDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
        public string Type { get; set; }
        public string ExpenseName { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool DidPay { get; set; }
    }
}
