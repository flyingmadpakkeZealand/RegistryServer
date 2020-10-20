using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistryRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private static Dictionary<string, List<FileEndPoint>> files = new Dictionary<string, List<FileEndPoint>>()
        {
            {"File1", new List<FileEndPoint>()
                {
                    new FileEndPoint(IPAddress.Loopback.ToString(), "4321"),
                    new FileEndPoint(IPAddress.Loopback.ToString(), "4322")
                }
            },
            {"File2", new List<FileEndPoint>()
                {
                    new FileEndPoint(IPAddress.Loopback.ToString(), "4321")
                }
            }
        };

        // GET: api/<FilesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FilesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FilesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FilesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
