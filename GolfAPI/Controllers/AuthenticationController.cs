using System;
using System.Collections.Generic;
using System.Linq;

using GolfAPI.Core.Contracts.Api;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly GolfDatabaseContext _context;

        public AuthenticationController(
            GolfDatabaseContext databaseContext
            )
        {
            _context = databaseContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("")]
        public ObjectResult Login(string username, string password)
        {
            var response = new ObjectResult("");
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var user = _context.Users.FirstOrDefault(u=> u.Username == username);
                //successful login
                if (user != null && user.Password.Equals(password))
                {
                    if (IsRoleEnabledToLogin(user.Role)) { 
                        response.Value = new ErrorResponse()
                        {
                            ErrorCode = ErrorCodes.NotPermission,
                            Description = "You are not enabled to enter the site"
                        };
                    }
                    else { 
                        response.Value = new LoginResponse()
                        {
                            Username = username,
                            Giud = user.Guid.ToString()
                        };
                    }
                }
                //user not found
                else if (user == null)
                {
                    response.Value = new ErrorResponse()
                        {
                            ErrorCode = ErrorCodes.WrongUsername,
                            Description = ErrorCodes.WrongUsername.ToString()
                        };
                }
                //wrong password
                else
                {
                    response.Value = new ErrorResponse()
                    {
                        ErrorCode = ErrorCodes.WrongPassword,
                        Description = ErrorCodes.WrongPassword.ToString()
                    };
                }
                
            }
            else
            {
                response.Value = new ErrorResponse()
                {
                    ErrorCode = ErrorCodes.BadRequest,
                    Description = "Invalid User/password"
                };
            }
            return response;
        }

        private bool IsRoleEnabledToLogin(RoleEnum role)
        {
            return role == RoleEnum.Manager;
        }
        
    }
}
