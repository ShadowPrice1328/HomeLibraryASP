using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Helpers
{
    public static class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new(obj);
            List<ValidationResult> validationResults = new();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults);

            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}