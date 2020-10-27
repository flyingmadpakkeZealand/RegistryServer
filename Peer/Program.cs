using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using ModelLib;

namespace Peer
{
    class Program
    {
        public const string REST_URL = "http://localhost:51309/api/Files/";
        public static IPAddress IP = IPAddress.Loopback;
        static void Main(string[] args)
        {
            int port = Convert.ToInt32(args[0]);
            Random rand = new Random(port);
            ServerWorker server = new ServerWorker(port);
            PeerConsoleMenu menu = new PeerConsoleMenu(server);
            Console.WriteLine("Open on port: " + args[0]);

            Task task = Task.Run(() => server.Start());

            //Console.WriteLine("Add a file using its path:");
            //menu.AddFile(Console.ReadLine()).Wait();

            //Console.Write("Lookup: ");
            //List<FileEndPoint> EndPoints = menu.LookupFile(Console.ReadLine()).Result;
            //FileEndPoint connection = EndPoints[rand.Next(0, EndPoints.Count)];

            //TcpClient client = new TcpClient();
            //client.Connect(connection.IpAddress, connection.Port);

            //Console.WriteLine(new StreamReader(client.GetStream()).ReadLine());
            
            PeerConsoleUI.Start(menu);

            menu.RemoveAllFiles().Wait();
            server.Stop();
            task.Wait();
        }
    }
}
