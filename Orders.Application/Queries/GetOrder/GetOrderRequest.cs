using System;

namespace Orders.Application.Queries.GetOrder
{
    public class GetOrderRequest: Lib.Application.RequestBase<GetOrderResponse>
    {
        public Guid OrderId { get; init; }
    }
}
