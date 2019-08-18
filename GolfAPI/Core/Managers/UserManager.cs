using GolfAPI.Configuration;
using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
using GolfAPI.Core.Factories;
using GolfAPI.DataLayer.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GolfAPI.Core.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _settings;
        public UserManager(
            IUserRepository userRepository,
            IOptions<AppSettings> appSettings
            )
        {
            _userRepository = userRepository;
            _settings = appSettings.Value;
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
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
                        result = GenerateUserToken(user);
                        
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

        private LoginResponse GenerateUserToken(User user)
        {

            var result = new LoginResponse()
            {
                Username = user.Username,
                Giud = user.Guid.ToString()
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            result.Token = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
