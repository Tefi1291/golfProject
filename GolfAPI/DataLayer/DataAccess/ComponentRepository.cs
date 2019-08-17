using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    public class ComponentRepository: BaseRepository, IComponentRepository
    {
        public ComponentRepository(GolfDatabaseContext context) : base(context)
        { }

        public async Task<int> AddNewComponentToOrder(OrderComponent data)
        {
            _context.OrdersComponent.Add(data);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            return 0;
        }

        public int CountByOrder(int orderId)
        {
            var components = _context.OrdersComponent.Where(oc => oc.Order.Id == orderId);
            return components.Count();
        }

        public async Task<int> DeleteComponentsFromOrder(int orderId, string[] componentCode)
        {

            foreach (var code in componentCode)
            {
                var q = from or in _context.OrdersComponent
                        join c in _context.Components
                        on or.OrderId equals orderId
                        where c.ComponentCode == code
                        select or;
                _context.OrdersComponent.RemoveRange(q);

            }

            var result = await _context.SaveChangesAsync();
            return result;
        }

        public DataModels.Component GetById(int id)
        {
            return _context.Components.First(c => c.Id == id);
        }

        public Dictionary<DataModels.Component, int> GetByOrderId(int orderId)
        {
            var result = new Dictionary<Component, int>();
            var componentsList = _context.OrdersComponent.Where(oc => oc.Order.Id == orderId);
            foreach (var or in componentsList)
            {
                var currentComponent = GetById(or.ComponentId);
                //if (result.ContainsKey(currentComponent))
                //{
                //    result[currentComponent] += 1;
                //}
                //else
                //{
                //    result.Add(currentComponent, 1);
                //}
                result.Add(currentComponent, or.ComponentQuantity);
               
            }
            return result;
        }

        public DataModels.Component GetComponentByCode(string code)
        {
           return _context.Components.FirstOrDefault(c => c.ComponentCode ==code);
        }

        public async Task<int> TryUpdateComponent(int orderId, Component component, int quantity, string description = "")
        {
            var result = _context.OrdersComponent.FirstOrDefault(
                oc => oc.ComponentId == component.Id && oc.OrderId == orderId
                );
            if (result != null)
            {
                _context.Entry(result).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                result.ComponentQuantity = quantity;

                await _context.SaveChangesAsync();
            }
            return 0;
        }
    }

    public interface IComponentRepository
    {
        Task<int> AddNewComponentToOrder(OrderComponent data);

        /// <summary>
        /// Get a Dictionary with all the components and a number of each one 
        /// for an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Dictionary<Component, int> GetByOrderId(int orderId);

        Component GetById(int id);

        int CountByOrder(int orderId);

        Component GetComponentByCode(string code);

        Task<int> TryUpdateComponent(int orderId, Component component, int quantity, string description = "");

        Task<int> DeleteComponentsFromOrder(int orderId, string[] componentCode);

    }
}
