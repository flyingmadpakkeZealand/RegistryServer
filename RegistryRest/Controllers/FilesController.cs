using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib;
using EnigmaLite;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistryRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private static char[] key;
        private static Random rand = new Random(DateTime.Now.Millisecond);
        private static Dictionary<string, HashSet<string>> files = new Dictionary<string, HashSet<string>>();
        public Dictionary<string, HashSet<string>> Files
        {
            get { return files; }
        }

        // GET: api/<FilesController>
        [HttpGet]
        [Route("{FileName}")]
        public IEnumerable<string> GetAll(string FileName)
        {
            if (files.ContainsKey(FileName))
            {
                return files[FileName];
            }
            else
            {
                return new List<string>();
            }
            
        }

        // POST api/<FilesController>
        [HttpPost]
        [Route("register/{fileName}")]
        public int Post(string fileName, [FromBody] string value)
        {
            if (files.ContainsKey(fileName))
            {
                return files[fileName].Add(value) ? 1 : 0;
            }

            files.Add(fileName, new HashSet<string>(){value});
            return 1;

            return -1;
        }

        [HttpPut]
        [Route("batchRemove")]
        public int BatchRemove([FromBody] List<string> files)
        {
            string peer = files[0];
            int count = 0;

            for (int i = 1; i < files.Count; i++)
            {
                count += DeRegister(files[i], peer);
            }

            return count;
        }

        // DELETE api/<FilesController>/5
        [HttpDelete]
        [Route("deregister/{filename}/{peer}")]
        public int DeRegister(string filename, string peer)
        {
            if (files.ContainsKey(filename))
            {
                int result = files[filename].Remove(peer) ? 1 : 0;
                if (files[filename].Count == 0)
                {
                    files.Remove(filename);
                }

                return result;
            }

            return 0;

            return -1;
        }

        [HttpGet]
        [Route("key")]
        public IEnumerable<char> GetKey()
        {
            char c1 = (char)rand.Next(33, 123);
            char c2 = (char)rand.Next(33, 123);
            char c3 = (char)rand.Next(33, 123);

            key = new[] {c1, c2, c3};

            return key;
        }

        [HttpPut]
        [Route("clean")]
        public int Clean([FromBody]string password)
        {
            if (key != null)
            {
                string check = "passWord";
                Enigma enigma = new Enigma(key);

                string dec = enigma.PermString(password);

                int length = dec[^1]-33;

                if (length < dec.Length)
                {
                    if (dec.Substring(0, length) == check)
                    {
                        files = new Dictionary<string, HashSet<string>>();
                        return 1;
                    }
                }
            }

            return 0;
        }

        [HttpGet]
        [Route("dir")]
        public IEnumerable<string> GetAllFileNames()
        {
            return files.Keys;
        }
    }
}
