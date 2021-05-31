using FluentValidation.Results;
using Lib.Domain;
using System.Collections.Generic;

namespace Lib.Application
{
    public class ResponseBase
    {
        internal ValidationResult ValidationResult { get; init; }

        public ResponseBase()
        {
            this.ValidationResult = new ValidationResult();
        }

        public ResponseBase(ValidationResult validationResult)
        {
            this.ValidationResult = validationResult ?? throw new System.ArgumentNullException(nameof(validationResult));
        }

        public bool IsValid => this.ValidationResult?.IsValid ?? true;

        public bool IsInvalid => !this.IsValid;

        public IEnumerable<ValidationFailure> Messages => this.ValidationResult?.Errors?.ToArray() ?? System.Array.Empty<ValidationFailure>();

        public void AddError(ValidationFailure validationFailure)
        {
            this.ValidationResult.Errors.Add(validationFailure);
        }

        public void AddError(Message errorMessage)
        {
            this.ValidationResult.Errors.Add(new ValidationFailure(null, errorMessage.Text)
            {
                ErrorCode = errorMessage.Text
            });
        }
    }
}
