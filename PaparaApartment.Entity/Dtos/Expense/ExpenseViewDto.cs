using System;
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Expense
{
    public class ExpenseViewDto:IDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int PaidCount { get; set; }
        public int UnPaidCount { get; set; }
    }
}
