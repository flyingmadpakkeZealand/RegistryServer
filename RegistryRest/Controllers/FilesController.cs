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
        private static Dictionary<string, List<FileEndPoint>> files = new Dictionary<string, List<FileEndPoint>>();

        public Dictionary<string, List<FileEndPoint>> Files
        {
            get { return files; }
        }

        // GET: api/<FilesController>
        [HttpGet]
        [Route("{FileName}")]
        public IEnumerable<FileEndPoint> GetAll(string FileName)
        {
            if (files.ContainsKey(FileName))
            {
                return files[FileName];
            }
            else
            {
                return null;
            }
            
        }

        // POST api/<FilesController>
        [HttpPost]
        [Route("register/{fileName}")]
        public int Post(string fileName, [FromBody] FileEndPoint value)
        {
            if (files.ContainsKey(fileName))
            {
                if (files[fileName].Find(endPoint=>endPoint.Port == value.Port && endPoint.IpAddress == value.IpAddress) == null )
                {
                    files[fileName].Add(value);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                files.Add(fileName, new List<FileEndPoint>(){value});
                return 1;
            }

            return -1;
        }

        // DELETE api/<FilesController>/5
        [HttpDelete("{id}")]
        [Route("deregister/{filename}")]
        public int DeRegister(string filename, [FromBody] FileEndPoint value)
        {
            if (files.ContainsKey(filename))
            {
                int index = files[filename].FindIndex(endpoint =>
                    endpoint.Port == value.Port && endpoint.IpAddress == value.IpAddress);
                if (index != -1)
                {
                    files[filename].RemoveAt(index);
                    if (files[filename].Count == 0)
                    {
                        files.Remove(filename);
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        
        }
    }
}
