
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.ApartmentExpense
{
    public class ApartmentExpenseAddDto:IDto
    {
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
    }
}
