using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Application
{
    public abstract class Handler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
        where TResponse : ResponseBase, new()
    {
        protected virtual Task<ValidationResult> DoValidateAsync(TRequest command) => Task<ValidationResult>.FromResult(new ValidationResult());

        protected abstract Task<TResponse> DoExecuteAsync(TRequest command);

        async Task<TResponse> IRequestHandler<TRequest, TResponse>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            var validate = await this.DoValidateAsync(request);

            if (!validate.IsValid)
            {
                return await validate.CreateResponseAsync<TResponse>();
            }

            return await this.DoExecuteAsync(request);
        }
    }
}
