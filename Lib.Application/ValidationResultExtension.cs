using FluentValidation.Results;
using System.Threading.Tasks;

namespace Lib.Application
{
    public static class ValidationResultExtension
    {
        public static TCommandResponse CreateResponse<TCommandResponse>(this ValidationResult validationResult)
            where TCommandResponse : ResponseBase, new()
        {
            return new TCommandResponse()
            {
                ValidationResult = validationResult
            };
        }

        public static Task<TCommandResponse> CreateResponseAsync<TCommandResponse>(this ValidationResult validationResult)
            where TCommandResponse : ResponseBase, new()
        {
            return Task.FromResult(new TCommandResponse()
            {
                ValidationResult = validationResult
            });
        }
    }
}
