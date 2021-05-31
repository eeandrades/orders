using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Orders.Application.Commands.Create;
using Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Application.Test
{
    [TestClass]
    public class OrderCreateTest
    {

        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IClientRepository> _clientRepositoryMock = new Mock<IClientRepository>();
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new Mock<IOrderRepository>();
        private readonly IRequestHandler<OrderCreateRequest, OrderCreateResponse> _orderCreateHandler;

        #region configure clients
        private readonly static Guid SClientExistsId = Guid.NewGuid();
        private readonly static Guid SClientNotFoundId = Guid.NewGuid();
        private readonly static Client SClientExists = new()
        {
            Id = SClientExistsId,
            Cpf = new Cpf("00972379657"),
            Name = "Client One"
        };

        #endregion


        #region configure products
        public readonly Product[] _products;

        #endregion
        private OrderCreateItem[] CreateValidOrdersItems()
        {
            var randomQuantity = new Random(100);

            return _products.Select(p => new OrderCreateItem()
            {
                ProductId = p.Id,
                Amount = randomQuantity.Next(1, 100)

            }).ToArray();

        }

        private OrderCreateItem[] CreateInvalidOrderItens()
        {
            return new OrderCreateItem[]
            {
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Amount = -1
                },
                new()
                {
                    ProductId = Guid.NewGuid(),
                    Amount = 0
                }
            };
        }

        private IEnumerable<Product> CreateProductsMock()
        {
            var priceRamdom = new Random(100);

            for (int i = 0; i <= 10; i++)
            {
                var product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product {i + 1}",
                    Price = Convert.ToDecimal(priceRamdom.NextDouble())
                };

                this._productRepositoryMock
                  .Setup(x => x.FindById(product.Id))
                  .Returns(Task.FromResult(product));


                this._productRepositoryMock
                  .Setup(x => x.Exists(product.Id))
                  .Returns(Task.FromResult(true));

                yield return product;

            }

        }

        public OrderCreateTest()
        {
            var orderCreateValidator = new OrderCreateValidator(this._clientRepositoryMock.Object, this._productRepositoryMock.Object);

            this._orderCreateHandler = new OrderCreateHandler(
                this._productRepositoryMock.Object,
                this._clientRepositoryMock.Object,
                this._orderRepositoryMock.Object,
                orderCreateValidator
             );

            this._clientRepositoryMock
                .Setup(x => x.FindById(SClientExistsId))
                .Returns(Task.FromResult( SClientExists));

            this._clientRepositoryMock
                .Setup(x => x.Exists(SClientExistsId))
                .Returns(Task.FromResult(true));


            _products = CreateProductsMock().ToArray();
        }        

        [TestMethod]
        async public Task Handle_Success()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = SClientExistsId;
            request.Items = CreateValidOrdersItems();
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert
            Assert.IsTrue(response.IsValid);
            Assert.IsTrue(response.OrderId != Guid.Empty);

            #endregion
        }

        [TestMethod]
        async public Task Handle_Error__WhenClientIdEmpty()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = Guid.Empty;
            request.Items = new OrderCreateItem[]
            {
                new()
                {
                    ProductId = Guid.Parse("e5636027-91fe-4629-a0d2-94a052b78507"),
                    Amount = 10
                }
            };
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert

            Assert.IsTrue(
                response.IsInvalid
                && response.Messages.Any(m => m.ErrorCode == OrderMessages.ClientIdEmpty.Code)
            );

            #endregion
        }

        [TestMethod]
        async public Task Handle_Error_WhenOrderMustHaveItem()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = SClientExistsId;
            request.Items = new OrderCreateItem[0];
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert

            Assert.IsTrue(
                response.IsInvalid
                && response.Messages.Any(m => m.ErrorCode == OrderMessages.OrderMustHaveItem.Code)
            );

            #endregion
        }

        [TestMethod]
        async public Task Handle_Error_WhenClientNotFound()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = SClientNotFoundId;
            request.Items = CreateValidOrdersItems();
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert

            Assert.IsTrue(
                response.IsInvalid
                && response.Messages.Any(m => m.ErrorCode == OrderMessages.ClientNotFound.Code)
            );

            #endregion
        }

        [TestMethod]
        async public Task Handle_Error_WhenProductNotFound()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = SClientNotFoundId;
            request.Items = CreateInvalidOrderItens();
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert

            Assert.IsTrue(
                response.IsInvalid
                && response.Messages.Any(m => m.ErrorCode == OrderMessages.ProductNotFound.Code)
            );

            #endregion
        }

        [TestMethod]
        async public Task Handle_Error_WhenAmountMustBeGreaterThanZero()
        {
            #region arrange

            var request = new OrderCreateRequest();
            request.ClientId = SClientNotFoundId;
            request.Items = CreateInvalidOrderItens();
            #endregion


            #region act
            var response = await this._orderCreateHandler.Handle(request, System.Threading.CancellationToken.None);
            #endregion

            #region assert

            Assert.IsTrue(
                response.IsInvalid
                && response.Messages.Any(m => m.ErrorCode == OrderMessages.AmountMustBeGreaterThanZero.Code)
            );

            #endregion
        }

    }
}
