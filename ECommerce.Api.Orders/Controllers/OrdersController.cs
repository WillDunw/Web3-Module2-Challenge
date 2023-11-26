using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{

    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	William Dunwoody - 2153053
* Date: 		12 November 2023
* Class Name: 	OrdersController.cs
* Description:  Provides an endpoint to get the orders by their customer ID. 
   */

    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {

        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider= ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetProductAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersAsync(customerId);
            if(result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }
    }
}
