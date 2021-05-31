using System;
using System.Threading.Tasks;

namespace Orders.Domain
{
    public interface IOrderQuery
    {
        Task<Order> GetById(Guid orderId);
    }
}
