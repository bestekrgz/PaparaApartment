using System;

using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entities.Dtos.Expense
{
    public class ExpenseAddForOneDto:IDto
    {
        public int ApartmentId { get; set; }
        public short TypeId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
