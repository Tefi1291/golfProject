using GolfAPI.Core.Contracts.Api;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Managers
{
    public interface IComponentOrderManager
    {
        IEnumerable<OrderComponent> BuildComponentsOrder(int orderId, ComponentApi[] components);

        Task<int> UpdateOrderComponents(int orderId, ComponentApi[] componentQuantity);
    }
}
