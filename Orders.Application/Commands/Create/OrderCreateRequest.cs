using Lib.Application;
using System;
using System.Collections.Generic;

namespace Orders.Application.Commands.Create
{
    public class OrderCreateRequest: RequestBase<OrderCreateResponse>
    {
        public Guid ClientId { get; set; }

        public IEnumerable<OrderCreateItem> Items { get; set; }
    }
}
