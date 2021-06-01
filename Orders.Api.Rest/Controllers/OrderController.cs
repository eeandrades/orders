using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Rest.Model;
using Orders.Api.Rest.Model.GetOrder;
using Orders.Api.Rest.Model.CreateOrder;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Api.Rest.Controllers
{
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {

        private ObjectResult CreateObjectResult(int successStatusCode, int errorStatusCode, ResponseModel response)
        {
            var statusCode = response.IsValid ? successStatusCode : errorStatusCode;
            return base.StatusCode(statusCode, response);
        }

        private ObjectResult CreateObjectResult(int successStatusCode, ResponseModel response)
        {
            return this.CreateObjectResult(successStatusCode, StatusCodes.Status400BadRequest, response);
        }

        private Guid GetUserId()
        {
            if (this.Request.Headers.TryGetValue("Token", out var token))
            {
                return Guid.Parse(token.ToString());
            }
            else
            {
                throw new Exception("Não autorizado");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateOrderResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
        public async Task<ObjectResult> CreateOrder([FromServices] IMediator mediator, [FromBody] CreateOrderRequestModel request)
        {
            //converte de model para application
            ///TODO: usar auto mapper
            var applicationRequest = new Application.Commands.Create.CreateOrderRequest()
            {
                ClientId = request.ClientId,
                Items = request.Items.Select(oi => new Application.Commands.Create.CreateOrderItemItem()
                {
                    Amount = oi.Amount,
                    ProductId = oi.ProductId
                }
                )
            };

            var applicationResponse = await mediator.Send(applicationRequest);


            ///TODO: usar auto mapper
            CreateOrderResponseModel response = new()
            {
                OrderId = applicationResponse.OrderId,
                OrderDate = applicationResponse.OrderDate,
                Messages = applicationResponse.Messages.Select(m => new UserMessage()
                {
                    Code = m.ErrorCode,
                    Kind = (UserMessageKind)m.Severity,
                    Message = m.ErrorMessage
                })
            };

            return this.CreateObjectResult(StatusCodes.Status201Created, response);
        }

        [HttpGet()]
        [Route("{orderId})")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderResponseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseModel))]
        public async Task<ObjectResult> GetOrder([FromServices] IMediator mediator, [FromRoute] Guid orderId)
        {
            //converte de model para application
            ///TODO: usar auto mapper
            var applicationRequest = new Application.Queries.GetOrder.GetOrderRequest
            {
                OrderId = orderId
            };

            var applicationResponse = await mediator.Send(applicationRequest);


            ///TODO: usar auto mapper
            GetOrderResponseModel response = new()
            {
                Order = new OrderModel()
                {
                    OrderId = applicationResponse.Order.Id,
                    OrderDate = applicationResponse.Order.OrderDate,
                    Client = new ClientModel()
                    {
                        Id = applicationResponse.Order.Client.Id,
                        Name = applicationResponse.Order.Client.Name,
                        Cpf = applicationResponse.Order.Client.Cpf.Value,
                    },

                    Items = applicationResponse.Order.Items.Select(ite => new OrderItemModel()
                    {
                        Amount = ite.Amount,
                        Product = new ProductModel()
                        {
                            Id = ite.Product.Id,
                            Name = ite.Product.Name,
                            Price = ite.Product.Price
                        },
                    }),
                },
                Messages = applicationResponse.Messages.Select(m => new UserMessage()
                {
                    Code = m.ErrorCode,
                    Kind = (UserMessageKind)m.Severity,
                    Message = m.ErrorMessage
                })
            };

            return this.CreateObjectResult(StatusCodes.Status201Created, response);
        }
    }
}
