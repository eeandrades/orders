using FluentValidation.Results;
using Lib.Application;
using Orders.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Application.Commands.Create
{
    public class OrderCreateHandler : Handler<OrderCreateRequest, OrderCreateResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderCreateValidator _orderCreateValidator;


        public OrderCreateHandler
            (IProductRepository productRepository, IClientRepository clientRepository, IOrderRepository orderRepository, OrderCreateValidator orderCreateValidator)
        {
            this._productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this._clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            this._orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this._orderCreateValidator = orderCreateValidator ?? throw new ArgumentNullException(nameof(orderCreateValidator));
        }

        protected override Task<ValidationResult> DoValidateAsync(OrderCreateRequest command) => this._orderCreateValidator.ValidateAsync(command);

        private Product GetProduct(Guid productId)
        {
            var product = this._productRepository.FindById(productId).Result;
            return product;
        }

        async protected override Task<OrderCreateResponse> DoExecuteAsync(OrderCreateRequest command)
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

            return new OrderCreateResponse()
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate
            };
        }
    }
}

