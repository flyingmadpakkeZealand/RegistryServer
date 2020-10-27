using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModelLib;

namespace Peer
{
    public class ServerWorker
    {
        private Dictionary<string, FileEndPoint> _providedFiles = new Dictionary<string, FileEndPoint>();
        private int _port;
        private HashSet<Task> _runningClients = new HashSet<Task>();
        private bool _open;

        public Dictionary<string, FileEndPoint> ProvidedFiles
        {
            get { return _providedFiles; }
        }

        public int Port
        {
            get { return _port; }
        }

        public ServerWorker(int port)
        {
            _port = port;
            _open = true;
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(Program.IP, _port);
            listener.Start();

            while (_open)
            {
                TcpClient client = listener.AcceptTcpClient();

                if (_open)
                {
                    Task task = Task.Run(() => HandleClient(client));
                    _runningClients.Add(task);
                    task.ContinueWith(t => _runningClients.Remove(task));
                }
                else
                {
                    client.Close();
                }
            }

            listener.Stop();
        }

        public void Stop()
        {
            _open = false;
            TcpClient stopClient = new TcpClient();
            stopClient.Connect(Program.IP, _port);
            Task.WaitAll(_runningClients.ToArray());
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            StreamWriter sw = new StreamWriter(stream);
            StreamReader sr = new StreamReader(stream);

            string fileName = sr.ReadLine();
            string path = ProvidedFiles[fileName].FullPath;

            sw.WriteLine(path);
            sw.Flush();
            client.Close();
        }
    }
}
