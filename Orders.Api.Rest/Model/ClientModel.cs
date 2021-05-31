using System;

namespace Orders.Api.Rest.Model
{
    public class ClientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Cpf { get; set; }

    }
}
