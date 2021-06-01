using FluentValidation;
using FluentValidation.Results;
using Lib.Domain;
using Orders.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Orders.Application.Commands.Create
{
    public class CreateOrderValidator: Validator<CreateOrderRequest>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderValidator(IClientRepository clientRepository, IProductRepository productRepository)
        {
            this._clientRepository = clientRepository ?? throw new System.ArgumentNullException(nameof(clientRepository));
            this._productRepository = productRepository ?? throw new System.ArgumentNullException(nameof(productRepository));
            RuleFor(o => o.ClientId)
                .NotEmpty()
                .WithMessage(OrderMessages.ClientIdEmpty);

            RuleFor(o => o.Items)
                .NotEmpty()
                .WithMessage(OrderMessages.OrderMustHaveItem);
        }

        public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateOrderRequest> context, CancellationToken cancellation = default)
        {
            var request = context.InstanceToValidate;

            ValidateClient(context, request);

            ValidateOrderItems(context, request);

            return base.ValidateAsync(context, cancellation);
        }

        async private void ValidateOrderItems(ValidationContext<CreateOrderRequest> context, CreateOrderRequest request)
        {
            foreach (var orderItem in request.Items)
            {
                if (!await this._productRepository.Exists(orderItem.ProductId))
                {
                    context.AddErrorMessage(OrderMessages.ProductNotFound);
                }

                if (orderItem.Amount <= 0)
                {
                    context.AddErrorMessage(OrderMessages.AmountMustBeGreaterThanZero);
                }
            }
        }

        async private void ValidateClient(ValidationContext<CreateOrderRequest> context, CreateOrderRequest request)
        {
            if (!await this._clientRepository.Exists(request.ClientId))
            {
                context.AddErrorMessage(OrderMessages.ClientNotFound);
            }
        }
    }
}
