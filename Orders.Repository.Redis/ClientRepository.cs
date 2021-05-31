using Orders.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Repository.Redis
{
    public class ClientRepository : IClientRepository
    {
        private readonly static Client[] SClients = new Client[]
        {
            new()
            {
                Id = Guid.Parse( "12fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("10975678957"),
                Name = "Cliente 1"
            },
            new()
            {
                Id = Guid.Parse("22fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("20975678957"),
                Name = "Cliente 2"
            },
            new()
            {
                Id = Guid.Parse( "32fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("30975678957"),
                Name = "Cliente 3"
            },
            new()
            {
                Id = Guid.Parse( "42fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("40975678957"),
                Name = "Cliente 4"
            },
            new()
            {
                Id = Guid.Parse( "52fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("50975678957"),
                Name = "Cliente 5"
            },
            new()
            {
                Id = Guid.Parse("62fce06e-b7d9-46ea-a5f2-1dfb3987bd7e"),
                Cpf = new Cpf("60975678957"),
                Name = "Cliente 6"
            }
        };

        Task<bool> IClientRepository.Exists(Guid clientId)
        {
            return Task.FromResult(SClients
                .Any(c => c.Id == clientId));
        }

        Task<Client> IClientRepository.FindById(Guid clientId)
        {
            return Task.FromResult(SClients
                .Where(c => c.Id == clientId)
                .DefaultIfEmpty(Client.Empty)
                .FirstOrDefault());
        }

    }
}
