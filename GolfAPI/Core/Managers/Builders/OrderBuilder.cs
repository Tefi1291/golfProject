using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers.Builders;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Managers.Builders
{
    public class OrderBuilder : IOrderBuilder
    {
        private readonly IUserRepository _userRepository;
        private readonly IComponentRepository _componentRepository;

        public OrderBuilder(
            IUserRepository userRepository,
            IComponentRepository componentRepository
            )
        {
            _userRepository = userRepository;
            _componentRepository = componentRepository;
        }

        public Order BuildOrderFromApi(OrderApi data)
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
