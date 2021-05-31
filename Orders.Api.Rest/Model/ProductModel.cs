using System;

namespace Orders.Api.Rest.Model
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

    }
}
