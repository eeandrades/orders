using FluentValidation.Results;
using Lib.Application;
using Orders.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Application.Commands.Create
{
    public class CreateOrderHandler : Handler<CreateOrderRequest, CreateOrderResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly CreateOrderValidator _orderCreateValidator;


        public CreateOrderHandler
            (IProductRepository productRepository, IClientRepository clientRepository, IOrderRepository orderRepository, CreateOrderValidator orderCreateValidator)
        {
            this._productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this._clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this._orderCreateValidator = orderCreateValidator ?? throw new ArgumentNullException(nameof(orderCreateValidator));
        }

        protected override Task<ValidationResult> DoValidateAsync(CreateOrderRequest command) => this._orderCreateValidator.ValidateAsync(command);

        private Product GetProduct(Guid productId)
        {
            var product = this._productRepository.FindById(productId).Result;
            return product;
        }

        async protected override Task<CreateOrderResponse> DoExecuteAsync(CreateOrderRequest command)
        {
            var client = await this._clientRepository.FindById(command.ClientId);

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                OrderDate = DateTime.Now,
                Client = client,
                Items =  command.Items.Select(oi => new OrderItem()
                {
                    
                    Product =GetProduct(oi.ProductId),
                    Amount = oi.Amount
                })
            };

            await this._orderRepository.Insert(order);

            return new CreateOrderResponse()
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate
            };
        }
    }
}

