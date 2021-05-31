using Lib.Domain;
using System;

namespace Orders.Domain
{
    public class Client: Entity<Guid>
    {
        public string Name { get; set; }

        public Cpf Cpf { get; set; } = new Cpf(string.Empty);

        public readonly static Client Empty = new Client()
        {
            Id = Guid.Empty,
            Name = string.Empty
        };

        public bool IsEmpty => this.Id == Empty.Id;

    }
}
