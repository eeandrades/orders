using System;
using System.Threading.Tasks;

namespace Orders.Domain
{
    public interface IClientRepository
    {
        Task<Client> FindById(Guid clientId);

        Task<bool> Exists(Guid clientId);
    }
}
