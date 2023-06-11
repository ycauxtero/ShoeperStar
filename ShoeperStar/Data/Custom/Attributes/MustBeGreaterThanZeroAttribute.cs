using System.ComponentModel.DataAnnotations;

namespace ShoeperStar.Data.Custom.Attributes
{
    public class MustBeGreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext ctx)
        {
            string errorMessage;

            if (value is null)
            {
                errorMessage = ErrorMessage ?? $"Please input value for {ctx.DisplayName}";
                return new ValidationResult(errorMessage);
            }

            var isANumber = double.TryParse(value.ToString(), out double parsedValue);

            if (!isANumber)
            {
                errorMessage = ErrorMessage ?? $"Please input a valid {ctx.DisplayName}";
                return new ValidationResult(errorMessage);
            }

            if (parsedValue <= 0)
            {
                errorMessage = ErrorMessage ?? $"{ctx.DisplayName} must be greater than zero";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
