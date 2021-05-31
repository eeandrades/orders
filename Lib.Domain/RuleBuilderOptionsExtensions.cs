using FluentValidation;
using FluentValidation.Results;

namespace Lib.Domain
{
    public static class RuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, Message message)
        {
            return  
                rule
                .WithErrorCode(message.Code)
                .WithMessage(message.Text);
        }
    }

    public static class ValidationContextExtensions
    {
        public static void AddErrorMessage<T>(this ValidationContext<T> validationContext, Message messge)
        {
            var failure = new ValidationFailure(string.Empty, messge.Text)
            {
                ErrorCode = messge.Code
            };

            validationContext.AddFailure(failure);
        }
    }
}
