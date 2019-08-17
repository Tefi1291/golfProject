using GolfAPI.Core.Contracts;
using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
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
        private readonly IUserRepository _userRepository;

        private readonly IComponentOrderManager _componentManager;
        private readonly IComponentRepository _componentRepository;

        public OrderManager(
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            IComponentOrderManager componentManager,
            IComponentRepository componentRepository
            )
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _componentRepository = componentRepository;
            _componentManager = componentManager;
        }

        public async Task<IEnumerable<OrderApi>> ProcessOrders(int? Id = null)
        {
            var result = new List<OrderApi>();
            // get orders from repository
            var orders = await GetOrdersFromRepository(Id);

            // Parser
            foreach(var currentOrder in orders.ToList())
            {
                var orderData = ParserCommonOrdersData(currentOrder);
                // get user that created the order
                orderData.User = ParserUserData(currentOrder);
                //Get build components from order
                var componentsDetails = ParserComponentsDetails(currentOrder);
                orderData.Components = (0 < componentsDetails.Count()) 
                                            ? componentsDetails.ToArray()
                                            : null;
                result.Add(orderData);
            }

            return result;  
        }

        public async Task<int> AddNewOrder(OrderApi data)
        {
            //if process ok -> return 0
            // error -> -1
            var result = 0;
            if (data != null)
            {
                

                var model = BuildOrder(data);
                
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
                //var model = BuildOrder(data);
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

        private UserApi ParserUserData(Order order)
        {
            var UserId = order.UserForeignKey;
            var currentUser = _userRepository.GetUserById(UserId);

            var result = new UserApi()
            {
                Id = currentUser.Id,
                Firstname = currentUser.Firstname,
                Lastname = currentUser.Lastname
            };

            return result;
        }

        private IEnumerable<ComponentApi> ParserComponentsDetails(Order order)
        {
            var result = new List<ComponentApi>();

            var components = _componentRepository.GetByOrderId(order.Id);
            foreach (var c in components)
            {
                result.Add(new ComponentApi()
                {
                    ComponentId = c.Key.Id,
                    ComponentCode = c.Key.ComponentCode,
                    Quantity = c.Value
                });
            }
            return result;
        }

        private OrderApi ParserCommonOrdersData(Order order)
        {
            //build result
            var result = new OrderApi()
            {
                Id = order.Id,
                DateCreated = order.DateCreated.ToString("dd-MM-yyyy"),
                DateRequired = order.DateRequired.ToString("dd-MM-yyyy"),
                Description = order.Description,
                OrderNumber = order.OrderNumber,
            };

            return result;
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

        // TODO refactor to builder 
        private Order BuildOrder(OrderApi data)
        {
            var user = _userRepository.GetUserByGuid(data.User.Guid);
            if (string.IsNullOrEmpty(data.User?.Guid) || user == null)
            {
                throw new Exception("The user not exist");
            }

            var model = new Order()
            {
                OrderNumber = data.OrderNumber,
                DateRequired = DateTime.Parse(data.DateRequired),
                DateCreated = DateTime.Now,
                Description = data.Description,
                CreatedBy = user,
                Components = new List<OrderComponent>()
            };

            if (data.Components != null && 0 < data.Components.Count())
            {
                foreach (var component in data.Components)
                {
                    var com = _componentRepository.GetComponentByCode(component.ComponentCode);
                    if (com == null)
                    {
                        com = new Component()
                        {
                            ComponentCode = component.ComponentCode
                        };
                    }
                    model.Components.Add(new OrderComponent()
                    {

                        Order = model,
                        Component = com,
                        ComponentQuantity = component.Quantity
                    });


                }

            }
            return model;
        }
    }
}
