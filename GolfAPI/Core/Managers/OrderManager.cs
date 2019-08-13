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
        private readonly IComponentRepository _componentRepository;

        public OrderManager(
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            IComponentRepository componentRepository
            )
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _componentRepository = componentRepository;
        }

        public async Task<IEnumerable<OrderApi>> ProcessOrders(int? Id = null)
        {
            // get orders from repository
            var orders = await GetOrdersFromRepository(Id);

            // Parser
            var result = ParserCommonOrdersData(orders); 
            //if id!=null process details of a particular order
            if (Id != null && 0 < orders.Count())
            {
                var currentOrder = orders.First();
                var orderData = result.ToList().First();

                // get user that created the order
                orderData.User = ParserUserData(currentOrder);
                //Get build components from order
                var componentsDetails = ParserComponentsDetails(currentOrder);
                orderData.Components = (0 < componentsDetails.Count()) 
                                            ? componentsDetails.ToArray()
                                            : null;

            }

            return result;  
        }

        private UserApi ParserUserData(Order order)
        {
            var UserId = order.UserForeignKey;
            var currentUser = _userRepository.GetUserById(UserId);
            
            var result = new UserApi()
            {
                Id = currentUser.Id,
                Firstname = currentUser.Firstname,
                LastName = currentUser.Lastname
            };

            return result; 
        }

        private IEnumerable<ComponentApiResponse> ParserComponentsDetails(Order order)
        {
            var result = new List<ComponentApiResponse>();

            var components = _componentRepository.GetByOrderId(order.Id);
            foreach (var c in components)
            {
                result.Add(new ComponentApiResponse()
                {
                    ComponentId = c.Key.Id,
                    ComponentCode = c.Key.ComponentCode,
                    Quantity = c.Value
                });
            }
            return result;
        }

        private IEnumerable<OrderApi> ParserCommonOrdersData(IEnumerable<Order> orders)
        {
            var result = new List<OrderApi>();
            foreach (var order in orders)
            {
                //build result
                result.Add(new OrderApi()
                {
                    Id = order.Id,
                    DateCreated = order.DateCreated.ToString("dd-MM-yy"),
                    DateRequired = order.DateRequired.ToString("dd-MM-yy"),
                    Description = order.Description,
                    OrderNumber = order.OrderNumber,
                });
            }
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
    }
}
