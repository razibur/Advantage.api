using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class ServerController: Controller
    {
        private readonly ApiContext ctx;

        public ServerController(ApiContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = this.ctx.Servers.OrderBy(s => s.Id).ToList();
            return Ok(response);
        }
        
        [HttpGet("{id}", Name="GetServer")]
        public IActionResult Get(int id)
        {
            var response = this.ctx.Servers.Find(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, [FromBody] ServerMessage msg)
        {
            var server = this.ctx.Servers.Find(id);

            if(server == null)
            {
                return NotFound();
            }

            // Refactor: move into a service
            if(msg.Payload == "activate")
            {
                server.IsOnline=true;
            }

            if(msg.Payload == "deactivate")
            {
                server.IsOnline=false;                
            }            

            this.ctx.SaveChanges();
            return new NoContentResult();
        }

    }
}