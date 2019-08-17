using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.DataAccess
{

    public interface IComponentRepository
    {
        /// <summary>
        /// Add an order component
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<int> AddNewComponentToOrder(OrderComponent data);

        /// <summary>
        /// Get a Dictionary with all the components and a number of each one 
        /// for an order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Dictionary<Component, int> GetByOrderId(int orderId);

        /// <summary>
        /// get a component by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Component GetById(int id);

        /// <summary>
        /// Get the number of components in a particular order
        /// </summary>
        /// <param name="orderId">order id</param>
        /// <returns></returns>
        int CountByOrder(int orderId);

        /// <summary>
        /// Gets a component by code
        /// if there is more than one component with same code
        /// return the first occurrence
        /// </summary>
        /// <param name="code">component code</param>
        /// <returns></returns>
        Component GetComponentByCode(string code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="component"></param>
        /// <param name="quantity"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<int> TryUpdateComponent(int orderId, Component component, int quantity, string description = null);

        /// <summary>
        /// Delete all the components from a list of component codes
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="componentCode"></param>
        /// <returns></returns>
        Task<int> DeleteComponentsFromOrder(int orderId, string[] componentCode);

    }
}
