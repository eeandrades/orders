using System;
using System.Threading.Tasks;

namespace Orders.Domain
{
    public interface IProductRepository
    {
        Task<Product> FindById(Guid productId);

        Task<bool> Exists(Guid productId);
    }
}
