using PaparaApartment.Entities.Dtos.ExpenseType;
using FluentValidation;

namespace PaparaApartment.Business.Validation
{
    public class ExpenseTypeValidator : AbstractValidator<ExpenseTypeAddDto>
    {
        public ExpenseTypeValidator()
        {
            RuleFor(et => et.Name).NotEmpty();
        }
    }
}
