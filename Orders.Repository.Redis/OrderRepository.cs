using Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Repository.Redis
{
    public class OrderRepository : IOrderRepository, IOrderQuery
    {
        private static readonly List<Order> SOrders = new List<Order>();

        Task<Order> IOrderQuery.GetById(Guid orderId)
        {
            return Task.FromResult( SOrders
                .Where(o => o.Id == orderId)
                .DefaultIfEmpty(Order.Empty)
                .FirstOrDefault());
        }

        Task<bool> IOrderRepository.Insert(Order order)
        {
            if (SOrders.Any(o => o.Id == order.Id))
                return Task.FromResult(false) ;

            SOrders.Add(order);
            return Task.FromResult(true);
        }
    }
}
