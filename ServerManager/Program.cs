using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EnigmaLite;
using ModelLib;

namespace ServerManager
{
    class Program
    {
        private const int PORT = 4000;
        private const string REST_URL = "http://localhost:51309/api/Files/";

        private const string FILE_PATH =
            "C:\\Users\\Bruger\\Desktop\\SWC\\mineProgrammer 2021\\RegistryServer\\Peer\\bin\\Debug\\netcoreapp3.1\\Peer.exe";

        static void Main(string[] args)
        {
            int peers = AssignPeers();

            ProcessStartInfo info = new ProcessStartInfo(FILE_PATH);
            info.UseShellExecute = true;
            info.CreateNoWindow = false;
            int count = 0;
            for (int i = 0; i < peers; i++)
            {
                count = TryStartPeer(info, count) + 1;
            }

            Console.Write("Type \"Confirm\" to reset Rest: ");
            if (Console.ReadLine() == "Confirm")
            {
                CleanRest().Wait();
            }
        }

        private static async Task CleanRest()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            Console.WriteLine("Requesting key...");
            Consumer<char[]> consumer = new Consumer<char[]>(REST_URL);
            char[] key = await consumer.GetOneAsync("key");
            Console.WriteLine("Key received: " + key[0] + key[1] + key[2]);

            Enigma enigma = new Enigma(key);
            char cipherLetter = (char)rand.Next(33, 123);
            string passWord = "passWord";
            StringBuilder builder = new StringBuilder();

            for (int i = passWord.Length; i < 20; i++)
            {
                builder.Append(cipherLetter);
            }

            passWord += builder.ToString() + Convert.ToChar(passWord.Length + 33);

            string dec = enigma.PermString(passWord);
            Console.WriteLine("Generated password: " + dec);
            Console.WriteLine("Sending password...");

            int response = await new Consumer<string>(REST_URL).PutAsync(dec, "clean");
            Console.WriteLine(response == 1 ? "Password accepted, Rest cleared" : "Password not accepted");
        }

        private static int Attempts;
        private static int TryStartPeer(ProcessStartInfo info, int portModifier)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Loopback, PORT + portModifier);
                listener.Start();
                listener.Stop();
                info.Arguments = PORT + portModifier + "";
                Process.Start(info);
            }
            catch (SocketException)
            {
                if(Attempts>50) throw new Exception("Too many closed ports");
                portModifier++;
                Attempts++;
                return TryStartPeer(info, portModifier);
            }

            Attempts = 0;
            return portModifier;
        }

        private static int AssignPeers()
        {
            string menuString = "Type peers to create (max 5): ";
            string confirmString = " (enter? / backspace?)";
            char input;
            char confirm;
            while (true)
            {
                input = 'x';
                confirm = 'x';
                while (input < 48 || input > 53)
                {
                    Console.Clear();
                    Console.Write(menuString);
                    input = Console.ReadKey().KeyChar;
                }

                while (!(confirm == (char)13 || confirm == (char)8))
                {
                    Console.Write(confirmString);
                    confirm = Console.ReadKey().KeyChar;
                    Console.Clear();
                    Console.Write(menuString + input);
                }

                if (confirm == (char) 13)
                {
                    Console.WriteLine();
                    return input - 48;
                }
            }
        }
    }
}
