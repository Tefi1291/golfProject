using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
using GolfAPI.Core.Factories;
using GolfAPI.DataLayer.DataModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfAPI.Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(
            IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
        }

        public object TryLoginUser(string username, string password)
        {
            object result = ErrorResponseFactory.BuildBadRequest();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = _userRepository.GetUserByUsername(username);
                //successful login
                if (user != null && user.Password.Equals(password))
                {
                    if (!IsEnabled(user))
                    {
                        result = ErrorResponseFactory.BuildNotPermission();
                    }
                    else
                    {
                        result = new LoginResponse()
                        {
                            Username = username,
                            Giud = user.Guid.ToString()
                        };
                    }
                }
                //user not found
                else if (user == null)
                {
                    result = ErrorResponseFactory.BuildWrongUser();
                }
                //wrong password
                else
                {
                    result = ErrorResponseFactory.BuildWrongPassword();
                }

            }
            return result;
        }

        private bool IsEnabled(User user)
        {
            //check conditions for login
            return user.Role == RoleEnum.Manager;
        }

    }
}
