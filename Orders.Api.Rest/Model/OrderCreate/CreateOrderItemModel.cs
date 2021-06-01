using System;

namespace Orders.Api.Rest.Model.CreateOrder
{
    public class CreateOrderItemModel
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
