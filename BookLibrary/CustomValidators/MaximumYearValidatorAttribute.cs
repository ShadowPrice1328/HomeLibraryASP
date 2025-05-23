using System.ComponentModel.DataAnnotations;

namespace BookLibrary.CustomValidators;

public class MaximumYearValidatorAttribute: ValidationAttribute
{
    public MaximumYearValidatorAttribute() 
    {
        MaximumYear = DateTime.Now.Year;
    }
    public string DefaultErrorMessage { get; set; } = "Year should be older than or equal to {0}";
    public int MaximumYear { get; set; }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            int providedYear = (int)value;

            if (providedYear > MaximumYear || providedYear < 1)
            {
                return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MaximumYear.ToString()), [nameof(validationContext.MemberName)]);
            }

            return ValidationResult.Success;
        }
        return null;
    }
}