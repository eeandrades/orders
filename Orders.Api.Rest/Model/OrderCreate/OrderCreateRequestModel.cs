using System;
using System.Collections.Generic;

namespace Orders.Api.Rest.Model.OrderCreate
{
    public class OrderCreateRequestModel
    { 
        public Guid ClientId { get; set; }

        public IEnumerable<OrderCreateItemModel> Items { get; set; }
    }
}
