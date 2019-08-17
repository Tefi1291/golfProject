using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.DataAccess
{
    public interface IUserRepository
    {
        User GetUserById(int Id);

        User GetUserByUsername(string username);

        User GetUserByGuid(string guid);
    }
}
