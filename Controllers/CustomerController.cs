using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[Controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext ctx;
        public CustomerController(ApiContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get(){
            var data = this.ctx.Customers.OrderBy(c => c.Id);

            return Ok(data);
        }

        // GET api/customer/5
        [HttpGet("{id}",Name="GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = this.ctx.Customers.Find(id);

            return Ok(customer);            
        }

        // GET api/customer/5
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null){
                return BadRequest();
            }

            this.ctx.Customers.Add(customer);
            this.ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);         
        }

    }
}