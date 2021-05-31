using System;

namespace Orders.Api.Rest.Model.OrderCreate
{
    public class OrderCreateItemModel
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
