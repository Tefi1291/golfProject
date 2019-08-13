using GolfAPI.Core.Contracts.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Managers
{
    public interface IOrderManager
    {
        Task<IEnumerable<OrderApi>> ProcessOrders(int? Id = null );
    }
}
