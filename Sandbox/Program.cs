using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static HashSet<Task> tasks = new HashSet<Task>();
        static void Main(string[] args)
        {
            for (int i = 0; i < 2; i++)
            {
                Task task = Task.Run(DoStuff);
                tasks.Add(task);
                task.ContinueWith(t=>tasks.Remove(task));
            }

            Task.WaitAll(tasks.ToArray());

            Thread.Sleep(1000);
            //IPAddress test = new IPAddress(new byte[]{127, 0, 0, 1});
            //string[] byteStrings = test.ToString().Split('.');
            //string hexString = "";

            //foreach (string byteString in byteStrings)
            //{
            //    byte b = Convert.ToByte(byteString);

            //    byte sig = (byte) (b / 16);
            //    byte lSig = (byte) (b % 16);

            //    char lSigChar = Convert.ToChar(lSig + 48 + (lSig > 9 ? 7 : 0));

            //    if (sig > 0)
            //    {
            //        char sigChar = Convert.ToChar(sig + 48 + (sig > 9 ? 7 : 0));
            //        hexString += "" + sigChar + lSigChar + ".";
            //    }
            //    else
            //    {
            //        hexString += "" + lSigChar + ".";
            //    }

                
            //}

            //hexString = hexString.TrimEnd('.');

            //Console.WriteLine(hexString);

            //string[] hexes = hexString.Split('.');

            //string number = "";
            //foreach (string hex in hexes)
            //{
            //    string currentNumber;
            //    int n1 = hex[0] - 48 - (hex[0] > 57 ? 7 : 0);

            //    if (hex.Length>1)
            //    {
            //        int n2 = hex[1] - 48 - (hex[1] > 57 ? 7 : 0);
            //        currentNumber = (n2 + n1*16).ToString();
            //    }
            //    else
            //    {
            //        currentNumber = n1.ToString();
            //    }

            //    number += currentNumber + ".";
            //}

            //number = number.TrimEnd('.');

            //Console.WriteLine(number);

            Console.ReadKey();
        }

        private static void DoStuff()
        {
            Thread.Sleep(10000);

        }
    }
}
