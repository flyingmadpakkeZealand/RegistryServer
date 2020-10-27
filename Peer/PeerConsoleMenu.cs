using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ModelLib;

namespace Peer
{
    public class PeerConsoleMenu
    {
        private ServerWorker _server;

        public int ServerPort
        {
            get { return _server.Port; }
        }

        public PeerConsoleMenu(ServerWorker server)
        {
            _server = server;
        }

        public async Task AddFile(string fullPath)
        {
            string[] pathArray = fullPath.Split('\\');

            string fileName = pathArray[^1];
            if (!_server.ProvidedFiles.ContainsKey(fileName))
            {
                _server.ProvidedFiles.Add(fileName, new FileEndPoint(fullPath));
            }

            Console.WriteLine(_server.ProvidedFiles[fileName].FullPath);
            Console.WriteLine(fileName);
            string ip = ToHexString(Program.IP);
            Console.WriteLine(ip);

            Consumer<string> consumer = new Consumer<string>(Program.REST_URL);
            await consumer.PostAsync(ip + ":" + _server.Port, "register/" + fileName);
        }

        /*
         * Remove file doesn't remove from local memory. If that is added, a quick check to see if file exist in ServerWorker
         * should be implemented because internet travel times might send over a delayed file request.
         *
         * Furthermore, multiple file location support? Different files with same ending (name)?
         */
        public async Task RemoveFile(string filename)
        {
            Consumer<string> consumer = new Consumer<string>(Program.REST_URL);
            string ip = ToHexString(Program.IP);
            await consumer.DeleteAsync("deregister/" + filename + "/" + ip + ":" + _server.Port);
        }

        public async Task RemoveAllFiles()
        {
            Consumer<List<string>> consumer = new Consumer<List<string>>(Program.REST_URL);
            string ip = ToHexString(Program.IP);

            List<string> files = new List<string>();
            files.Add(ip + ":" + _server.Port);

            foreach (string file in _server.ProvidedFiles.Keys)
            {
                files.Add(file);
            }

            await consumer.PutAsync(files, "batchRemove");
        }

        public async Task<List<FileEndPoint>> LookupFile(string fileName)
        {
            Consumer<string> consumer = new Consumer<string>(Program.REST_URL);
            List<string> hexIpsAndPorts = await consumer.GetAsync(fileName);

            List<FileEndPoint> endPoints = new List<FileEndPoint>();
            foreach (string hexIpAndPort in hexIpsAndPorts)
            {
                endPoints.Add(ToFileEndPoint(hexIpAndPort));
            }

            return endPoints;
        }

        public async Task<List<string>> LookupAllFileNames()
        {
            Consumer<string> consumer = new Consumer<string>(Program.REST_URL);
            List<string> fileNames = await consumer.GetAsync("dir");
            return fileNames;
        }

        private FileEndPoint ToFileEndPoint(string hexAndPort)
        {
            string[] split = hexAndPort.Split(':');
            string port = split[1];
            string[] hexStrings = split[0].Split('.');
            List<byte> bytes = new List<byte>(4);

            foreach (string hexString in hexStrings)
            {
                int number;
                int n1 = hexString[0] - 48 - (hexString[0] > 57 ? 7 : 0);

                if (hexString.Length > 1)
                {
                    int n2 = hexString[1] - 48 - (hexString[1] > 57 ? 7 : 0);
                    number = n1 * 16 + n2;
                }
                else
                {
                    number = n1;
                }
                bytes.Add((byte)number);
            }

            return new FileEndPoint(new IPAddress(bytes.ToArray()), Convert.ToInt32(port));
        }

        private string ToHexString(IPAddress iPAddress)
        {
            string[] byteStrings = iPAddress.ToString().Split('.');
            string hexString = "";

            foreach (string byteString in byteStrings)
            {
                byte b = Convert.ToByte(byteString);

                byte sig = (byte)(b / 16);
                byte lSig = (byte)(b % 16);

                char lSigChar = Convert.ToChar(lSig + 48 + (lSig > 9 ? 7 : 0));

                if (sig > 0)
                {
                    char sigChar = Convert.ToChar(sig + 48 + (sig > 9 ? 7 : 0));
                    hexString += "" + sigChar + lSigChar + ".";
                }
                else
                {
                    hexString += "" + lSigChar + ".";
                }
            }

            return hexString.TrimEnd('.');
        }
    }
}
