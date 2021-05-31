using FluentValidation.Results;

namespace Lib.Domain
{
    public static class ValidationResultExtension
    {
        public static ValidationResult AddError(this ValidationResult validationResult, string propertyName, string errorMessage)
        {
            validationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
            return validationResult;
        }

        public static ValidationResult AddError(this ValidationResult validationResult, string errorMessage)
        {
            return validationResult.AddError(string.Empty, errorMessage);
        }

        public static void Merge(this ValidationResult source, ValidationResult dest)
        {
            foreach (var err in dest.Errors)
            {
                source.Errors.Add(err);
            }
        }
    }
}
