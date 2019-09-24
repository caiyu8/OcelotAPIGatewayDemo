using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        // GET: api/Client
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "client1", "client2" };
        }

        // GET: api/Client/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"Client {id}";
        }
    }
}
