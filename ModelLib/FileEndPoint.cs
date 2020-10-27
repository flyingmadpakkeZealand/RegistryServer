using System;
using System.Net;

namespace ModelLib
{
    public class FileEndPoint
    {
        private IPAddress _ipAddress;
        private int _port;
        private string _fullPath;

        public string FullPath
        {
            get => _fullPath;
            set => _fullPath = value;
        }

        public IPAddress IpAddress
        {
            get => _ipAddress;
            set => _ipAddress = value;
        }

        public int Port
        {
            get => _port;
            set => _port = value;
        }

        public FileEndPoint()
        {

        }

        public FileEndPoint(IPAddress ipAddress, int port, string fullPath)
        {
            _ipAddress = ipAddress;
            _port = port;
            _fullPath = fullPath;
        }

        public FileEndPoint(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public FileEndPoint(string fullPath)
        {
            _fullPath = fullPath;
        }
    }
}
