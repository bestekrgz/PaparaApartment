using FluentValidation;


namespace PaparaApartment.Core.CrossCuttingConcern
{
    public static class ValidatorTool
    {
       

        public static void Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
