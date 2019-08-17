using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GolfAPI.Core.Contracts.Api;
using GolfAPI.Core.Contracts.Managers;
using Microsoft.AspNetCore.Cors;

namespace GolfAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularApp")] // allows our angular app to communicate with the controller
    public class OrdersController : ControllerBase
    {
        private readonly IOrderManager _manager;
        public OrdersController(IOrderManager manager)
        {
            _manager = manager;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderApi>>> GetOrders()
        {
            var result = new OkObjectResult("");
            //get orders from manager
            var response = await _manager.ProcessOrders();
            result.Value = response;

            return result;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderApi>> GetOrder(int id)
        {
            
            var response = await _manager.ProcessOrders(id);

            var order = response.FirstOrDefault(o => o.Id == id);
            var result = (order != null) ?
                new ObjectResult(order) :
                new NotFoundObjectResult("");
           
            return result;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderApi order)
        {
            //if id doesn't match -> 400 response
            if (id != order.Id)
            {
                return BadRequest();
            }

            var result = await _manager.UpdateOrder(order);

            return new OkObjectResult(result);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder(OrderApi order)
        {
            ///if a controller has [ApiControlller] attribute, its binds invalid 
            ///model parameter to 400 response
            var result = 0;
            
            try
            {

                result = await _manager.AddNewOrder(order);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                result = -1;
            }

            return new ObjectResult(result);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderApi>> DeleteOrder(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            //if (order == null)
            //{
            //    return NotFound();
            //}

            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();

            return null;
        }

        private bool OrderExists(int id)
        {
            return true;
        }
    }
}
