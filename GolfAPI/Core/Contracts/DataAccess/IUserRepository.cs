using GolfAPI.DataLayer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Contracts.DataAccess
{
    /// <summary>
    /// Access to Users' Data
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="Id">user id</param>
        /// <returns></returns>
        User GetUserById(int Id);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username">username</param>
        /// <returns></returns>
        User GetUserByUsername(string username);

        /// <summary>
        /// Get user by Guid
        /// </summary>
        /// <param name="guid">guid as string</param>
        /// <returns></returns>
        User GetUserByGuid(string guid);
    }
}
