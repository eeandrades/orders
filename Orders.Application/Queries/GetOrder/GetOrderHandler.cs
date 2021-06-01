using Orders.Domain;
using System;
using System.Threading.Tasks;

namespace Orders.Application.Queries.GetOrder
{
    public class GetOrderHandler: Lib.Application.Handler<GetOrderRequest, GetOrderResponse>
    {
        private readonly IOrderQuery _orderQuery;

        public GetOrderHandler(IOrderQuery orderQuery)
        {
            this._orderQuery = orderQuery ?? throw new ArgumentNullException(nameof(orderQuery));
        }

        async protected override Task<GetOrderResponse> DoExecuteAsync(GetOrderRequest command)
        {
            var response = new GetOrderResponse()
            {
                Order = await this._orderQuery.GetById(command.OrderId)
            };            

            if (response.Order.IsEmpty)
            {
                response.AddError(OrderMessages.OrderNotFount);
            }
            return response;
        }
    }
}


