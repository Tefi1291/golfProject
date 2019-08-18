using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.Managers
{
    public interface IUserManager
    {
        object TryLoginUser(string username, string password);
    }
}
