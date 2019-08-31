using System.ComponentModel.DataAnnotations;

namespace JuicyBurger.Services.ValidationAttributes
{
    public class AtLeastOneIngredient : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Choose at least one ingredient!");
            }

            return ValidationResult.Success;
        }
    }
}
