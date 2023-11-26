using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{

    /*
* Course: 		Web Programming 3
* Assessment: 	Milestone 3
* Created by: 	William Dunwoody - 2153053
* Date: 		12 November 2023
* Class Name: 	CustomerController.cs
* Description:  Provides endpoints to retrieve the customer information from the database. 
  */

    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;

        public CustomerController(ICustomersProvider provider)
        {
            this.customersProvider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await customersProvider.GetCustomersAsync();

            if(result.IsSuccess)
            {
                return Ok(result.Customers);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var result = await customersProvider.GetCustomerAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }

            return NotFound();
        }
    }
}
