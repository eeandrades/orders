using Lib.Application;
using System;
using System.Collections.Generic;

namespace Orders.Application.Commands.Create
{
    public class CreateOrderRequest : RequestBase<CreateOrderResponse>
    {
        public Guid ClientId { get; set; }

        public IEnumerable<CreateOrderItemItem> Items { get; set; }
    }
}
