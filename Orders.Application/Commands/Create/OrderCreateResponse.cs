using Lib.Application;
using System;

namespace Orders.Application.Commands.Create
{
    public class OrderCreateResponse: ResponseBase
    {
        public Guid OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
