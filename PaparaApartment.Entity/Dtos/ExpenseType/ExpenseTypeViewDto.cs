using PaparaApartment.Core.Entities;


namespace PaparaApartment.Entities.Dtos.ExpenseType
{
    public class ExpenseTypeViewDto : IDto
    {
        public short Id { get; set; }
        public string Name { get; set; }
    }
}