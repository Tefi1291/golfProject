using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.DataAccess
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        Task<Order> GetOrderById(int id);

        Task addOrder(Order order);

        Task UpdateOrder(Order order);

        Task TryUpdateOrder(Order order, 
            string OrderNumber, 
            DateTime DateRequired,
            string Description);
    }
}
