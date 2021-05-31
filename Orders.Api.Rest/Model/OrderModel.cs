using Orders.Api.Rest.Model;
using System;
using System.Collections.Generic;

namespace Orders.Api.Rest.Model
{
    public class OrderModel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; init; }

        public ClientModel Client { get; init; }

        public IEnumerable<OrderItemModel> Items { get; init; }
    }
}
