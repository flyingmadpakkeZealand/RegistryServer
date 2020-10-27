using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using ModelLib;

namespace Peer
{
    public static class PeerConsoleUI
    {
        private const string MENU_STRING = "1) Try find file\n" +
                                           "2) Add file\n" +
                                           "3) Remove file\n" +
                                           "4) See avaible files\n" +
                                           "Please type a number (ESC to quit): ";
        public static void Start(PeerConsoleMenu menu)
        {
            bool active = true;
            while (active)
            {
                char input = 'x';
                while ((input < 49 || input> 53) && input != 27)
                {
                    Console.Clear();
                    Console.WriteLine("Peer on port: " + menu.ServerPort);
                    Console.Write(MENU_STRING);
                    input = Console.ReadKey().KeyChar;
                }

                Console.WriteLine();

                switch (input)
                {
                    case '1': FindFile(menu); break;
                    case '2': AddFile(menu); break;
                    case '3': RemoveFile(menu); break;
                    case '4': GetAllFileNames(menu); break;
                    default: active = false; break;
                }
            }
        }

        private static void GetAllFileNames(PeerConsoleMenu menu)
        {
            Console.WriteLine("Getting all file names...");
            List<string> fileNames = menu.LookupAllFileNames().Result;
            Console.WriteLine("Found file names: ");

            if (fileNames.Count > 0)
            {
                Console.WriteLine();
                foreach (string fileName in fileNames)
                {
                    Console.WriteLine(fileName);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No files currently exists, use Add file to make files available for download");
            }

            Continue();
        }

        private static void Continue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void FindFile(PeerConsoleMenu menu)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Console.Write("Type filename: ");
            string fileName = Console.ReadLine();
            Console.WriteLine("Trying to locate file...");
            List<FileEndPoint> endPoints = menu.LookupFile(fileName).Result;

            if (endPoints.Count == 0)
            {
                Console.WriteLine(fileName + " could not be found");
            }
            else
            {
                Console.WriteLine("File found, connecting to peer...");
                FileEndPoint fileEndPoint = endPoints[rand.Next(0, endPoints.Count)];
                Console.WriteLine("Peer data: " + fileEndPoint.IpAddress + ":" + fileEndPoint.Port);
                TcpClient client = new TcpClient();
                client.Connect(fileEndPoint.IpAddress, fileEndPoint.Port);

                NetworkStream stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream);
                StreamReader sr = new StreamReader(stream);

                sw.WriteLine(fileName);
                sw.Flush();
                string mockup = sr.ReadLine();

                Console.WriteLine("Answer from peer: ");
                Console.WriteLine("Mockup: " + mockup);
            }

            Continue();
        }

        private static void AddFile(PeerConsoleMenu menu)
        {
            Console.WriteLine("Please type the full path of the file, including file name: ");
            string path = Console.ReadLine();
            if (path.Contains('\\') && !path.EndsWith('\\'))
            {
                Console.WriteLine("Adding file to server...");

                menu.AddFile(path).Wait();
                Console.WriteLine("File added.");
            }
            else
            {
                Console.WriteLine("Please provide a valid file path.");
            }

            Continue();
        }

        private static void RemoveFile(PeerConsoleMenu menu)
        {
            Console.Write("Write file name to remove: ");
            string fileName = Console.ReadLine();
            Console.WriteLine("Trying to remove: " + fileName);

            menu.RemoveFile(fileName).Wait();
            Console.WriteLine(fileName + " was removed.");
            Continue();
        }
    }
}
