using System;
using System.Collections.Generic;

namespace Orders.Api.Rest.Model.CreateOrder
{
    public class CreateOrderRequestModel
    { 
        public Guid ClientId { get; set; }

        public IEnumerable<CreateOrderItemModel> Items { get; set; }
    }
}
