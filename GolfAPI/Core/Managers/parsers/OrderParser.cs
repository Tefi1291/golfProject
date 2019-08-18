using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers.Parsers;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Managers.parsers
{
    public class OrderParser : IOrderParser
    {
        private readonly IComponentRepository _componentRepository;
        private readonly IUserRepository _userRepository;

        public OrderParser(
            IComponentRepository componentRepository,
            IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
            _componentRepository = componentRepository;

        }

        public IEnumerable<OrderApi> ParserOrdersToApi(IEnumerable<Order> orders)
        {
            var result = new List<OrderApi>();
            foreach (var currentOrder in orders.ToList())
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

    }
}
