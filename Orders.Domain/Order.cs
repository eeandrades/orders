using Lib.Domain;
using System;
using System.Collections.Generic;

namespace Orders.Domain
{
    public class Order: RootAggregator<Guid>
    {
        public DateTime OrderDate { get; init; }

        public Client Client { get; init; }

        public IEnumerable<OrderItem> Items { get; init; } = new OrderItem[0];

        public readonly static Order Empty = new Order()
        {
            Id = Guid.Empty,
            Client = Client.Empty
        };

        public bool IsEmpty => this.Id == Empty.Id;

    }
}
