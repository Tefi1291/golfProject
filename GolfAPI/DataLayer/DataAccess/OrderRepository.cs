using GolfAPI.Core.Contracts;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    public class OrderRepository: BaseRepository, IOrderRepository
    {
        public OrderRepository(GolfDatabaseContext context) : base(context)
        { }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.AsEnumerable();
        }
    }
}
