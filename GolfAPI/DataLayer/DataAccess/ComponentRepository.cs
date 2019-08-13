using GolfAPI.DataLayer.ADL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.DataLayer.DataAccess
{
    public class ComponentRepository: BaseRepository, IComponentRepository
    {
        public ComponentRepository(GolfDatabaseContext context) : base(context)
        { }

        public int CountByOrder(int orderId)
        {
            var components = _context.OrdersComponent.Where(oc => oc.Order.Id == orderId);
            return components.Count();
        }

        public DataModels.Component GetById(int id)
        {
            return _context.Components.First(c => c.Id == id);
        }

        public Dictionary<DataModels.Component, int> GetByOrderId(int orderId)
        {
            var result = new Dictionary<DataModels.Component, int>();
            var componentsList = _context.OrdersComponent.Where(oc => oc.Order.Id == orderId);
            foreach (var or in componentsList)
            {
                var currentComponent = GetById(or.ComponentId);
                if (result.ContainsKey(currentComponent))
                {
                    result[currentComponent] += 1;
                }
                else
                {
                    result.Add(currentComponent, 1);
                }
               
            }
            return result;
        }
    }

    public interface IComponentRepository
    {
        /// <summary>
        /// Get a Dictionary with all the components and a number of each one 
        /// for an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Dictionary<DataModels.Component, int> GetByOrderId(int orderId);

        DataModels.Component GetById(int id);

        int CountByOrder(int orderId);
    }
}
