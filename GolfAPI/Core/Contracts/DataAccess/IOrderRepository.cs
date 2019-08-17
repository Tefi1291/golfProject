using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.DataAccess
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Get all orders from all users
        /// </summary>
        /// <returns></returns>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// Get a particular order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Order> GetOrderById(int id);

        /// <summary>
        /// add an order to the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task addOrder(Order order);

        
        /// <summary>
        /// Try update order in database
        /// If order not exists, throw an exception
        /// </summary>
        /// <param name="order">order model</param>
        /// <param name="OrderNumber">order number</param>
        /// <param name="DateRequired">date required order</param>
        /// <param name="Description">order description</param>
        /// <returns></returns>
        Task TryUpdateOrder(Order order, 
            string OrderNumber, 
            DateTime DateRequired,
            string Description);
    }
}
