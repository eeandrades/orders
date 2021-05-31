using Lib.Domain;
using System;

namespace Orders.Domain
{
    public class Product: Entity<Guid>
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public readonly static Product Empty = new Product()
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Price = 0
        };

        public bool IsEmpty => this.Id == Empty.Id;
    }
}
