using System.Threading.Tasks;

namespace Orders.Domain
{
    public interface IOrderRepository
    {
        Task<bool> Insert(Order order);
    }
}
