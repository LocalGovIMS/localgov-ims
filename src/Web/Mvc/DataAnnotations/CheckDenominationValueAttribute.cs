using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Mvc.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CheckDenominationValueAttribute : ValidationAttribute
    {
        private readonly decimal _denominationValue;

        public CheckDenominationValueAttribute(double denominationValue)
            : base("{0} is not a valid value for this denomination")
        {
            _denominationValue = Convert.ToDecimal(denominationValue);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var errorMessage = FormatErrorMessage((validationContext.DisplayName));

            if (value == null) return ValidationResult.Success;

            var amount = (decimal)value;
            bool result = (amount % _denominationValue) == 0;
            if (result) return ValidationResult.Success;

            return new ValidationResult(errorMessage);
        }
    }
}