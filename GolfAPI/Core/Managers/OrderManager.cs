using GolfAPI.Core.Contracts;
using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
using GolfAPI.Core.Contracts.Managers.Builders;
using GolfAPI.Core.Contracts.Managers.Parsers;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataAccess;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfAPI.Core.Managers
{
    public class OrderManager: IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderParser _orderParser;
        private readonly IOrderBuilder _orderBuilder;

        private readonly IComponentOrderManager _componentManager;

        public OrderManager(
            IOrderRepository orderRepository,
            IOrderParser orderParser,
            IOrderBuilder orderBuilder,
            IComponentOrderManager componentManager

            )
        {
            _orderRepository = orderRepository;
            _componentManager = componentManager;

            _orderParser = orderParser;
            _orderBuilder = orderBuilder;
        }

        public async Task<IEnumerable<OrderApi>> ProcessOrders(int? Id = null)
        {
            // get orders from repository
            var orders = await GetOrdersFromRepository(Id);
            // Parser
            var result = _orderParser.ParserOrdersToApi(orders);
            return result;  
        }

        public async Task<int> AddNewOrder(OrderApi data)
        {
            //if process ok -> return 0
            // error -> -1
            var result = 0;
            if (data != null)
            {

                var model = _orderBuilder.BuildOrderFromApi(data);
                
                try
                {
                    await _orderRepository.addOrder(model);
                }
                catch (Exception databaseException)
                {
                    Console.WriteLine(databaseException.Message);
                    result = -1;
                }
            }
            return result;
        }

        public async Task<int> UpdateOrder(OrderApi data)
        {
            if (data != null)
            {
                try
                {
                    //get the order
                    var model = await _orderRepository.GetOrderById(data.Id);
                    //update order
                    await _orderRepository.TryUpdateOrder(model,
                        data.OrderNumber,
                        DateTime.Parse(data.DateRequired),
                        data.Description
                        );

                    // try update order components
                    var updatedComponentsResult = await _componentManager.UpdateOrderComponents(data.Id, data.Components);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return -1;
                }
            }
            return 0;
        }

        private async Task<IEnumerable<Order>> GetOrdersFromRepository(int? orderId = null)
        {
            var ordersModel = new List<Order>();
            if (orderId != null)
            {
                var orderModel = await _orderRepository.GetOrderById(orderId.Value);
                if (orderModel != null) ordersModel.Add(orderModel);
            }
            else
            {
                ordersModel = _orderRepository.GetOrders().ToList();
            }

            return ordersModel;
        }

        
    }
}
