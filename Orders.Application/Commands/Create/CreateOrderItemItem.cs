using System;

namespace Orders.Application.Commands.Create
{
    public class CreateOrderItemItem
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
