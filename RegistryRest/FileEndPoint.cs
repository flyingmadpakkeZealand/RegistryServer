using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryRest
{
    public class FileEndPoint
    {
        private string _ipAddress;
        private string _port;

        public string IpAddress
        {
            get => _ipAddress;
            set => _ipAddress = value;
        }

        public string Port
        {
            get => _port;
            set => _port = value;
        }

        public FileEndPoint()
        {
            
        }

        public FileEndPoint(string ipAddress, string port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }
    }
}
