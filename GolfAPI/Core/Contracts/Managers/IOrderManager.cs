using GolfAPI.Core.Contracts.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Managers
{
    public interface IOrderManager
    {
        /// <summary>
        /// Process orders, 
        /// if id's value, will process order with Id=id,
        /// else will process all orders 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderApi>> ProcessOrders(int? Id = null );

        Task<int> AddNewOrder(OrderApi data);

        Task<int> UpdateOrder(OrderApi data);
    }
}
