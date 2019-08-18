using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.DataAccess;
using GolfAPI.Core.Contracts.Managers;
using GolfAPI.DataLayer.ADL;
using GolfAPI.DataLayer.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthenticationController(
            IUserManager userManager
            )
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ObjectResult> Login(string username, string password)
        {
            var result = new ObjectResult("");
            try
            {
                var response = _userManager.TryLoginUser(username, password);
                result = new OkObjectResult(response);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = new BadRequestObjectResult("");
            }
            
            return result;
        }

        
    }
}
