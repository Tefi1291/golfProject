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

        public async Task addOrder(Order order)
        {
            try
            {
               await _context.AddAsync(order);
               
               var result = await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public async Task<Order> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.AsEnumerable();
        }

        public async Task TryUpdateOrder(Order order, string OrderNumber, DateTime DateRequired, string Description)
        {
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            order.OrderNumber = OrderNumber;
            order.DateRequired = DateRequired;
            order.Description = Description;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
