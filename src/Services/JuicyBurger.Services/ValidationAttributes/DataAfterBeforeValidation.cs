using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace JuicyBurger.Services.ValidationAttributes
{
    public class DataAfterBeforeValidation : ValidationAttribute
    {
        private readonly string dateAfter;

        public DataAfterBeforeValidation(string dateAfter)
        {
            this.dateAfter = dateAfter;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currDate = DateTime.UtcNow.Date;

            if (!(value is DateTime))
            {
                return new ValidationResult("Invalid " + validationContext.DisplayName);
            }

            var startDateTimeValue = (DateTime)value;
            var endDateTimeValue = (DateTime)validationContext.ObjectType.GetProperty(this.dateAfter)
                .GetValue(validationContext.ObjectInstance);

            if (startDateTimeValue.Date < currDate)
            {
                return new ValidationResult(validationContext.DisplayName + " is before " + currDate
                    .ToString("dd/MM/yyyy", CultureInfo.CurrentCulture));
            }

            if (startDateTimeValue.Date > endDateTimeValue)
            {
                return new ValidationResult(validationContext.DisplayName + " is greater than " +
                    endDateTimeValue.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture));
            }

            return ValidationResult.Success;
        }
    }
}