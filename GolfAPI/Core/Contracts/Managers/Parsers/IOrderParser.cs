using GolfAPI.Core.Contracts.Api;
using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Managers.Parsers
{
    public interface IOrderParser
    {
        IEnumerable<OrderApi> ParserOrdersToApi(IEnumerable<Order> orders);
    }
}
