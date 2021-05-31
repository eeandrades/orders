using System;

namespace Orders.Application.Commands.Create
{
    public class OrderCreateItem
    {
        public Guid ProductId { get; set; }

        public int Amount { get; set; }
    }
}
