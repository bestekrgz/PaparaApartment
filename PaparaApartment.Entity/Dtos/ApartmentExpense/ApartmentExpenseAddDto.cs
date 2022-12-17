using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaApartment.Core.Entities;

namespace PaparaApartment.Entity.Dtos.ApartmentExpense
{
    public class ApartmentExpenseAddDto:IDto
    {
        public int ApartmentId { get; set; }
        public int ExpenseId { get; set; }
    }
}
