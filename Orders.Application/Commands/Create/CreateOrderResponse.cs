using Lib.Application;
using System;

namespace Orders.Application.Commands.Create
{
    public class CreateOrderResponse : ResponseBase
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
