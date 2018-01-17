using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace HorseCreator
{
   
    class Program
    {
        static Socket clientSocket;
        static Thread generatingThread;
        static string horse = "";
        static Random rnd = new Random();
        static string[] names = new string[] { "Speedy", "Mr. Bell", "Cheesy", "Nugget", "Racer", "Plötze", "Hinkebein", "Laggy", "Buggy", "Wormy" };
        static int i = 1;

        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Loopback, 10100);
            clientSocket = client.Client;

            generatingThread = new Thread(StartGenerating);
            generatingThread.IsBackground = true;
            generatingThread.Start();

            while (true) ;
        }

        static private void StartGenerating()
        {
            while (generatingThread.IsAlive)
            {
                horse = names[rnd.Next(0, 9)] + ":" + rnd.Next(40, 200);
                Generate(horse);
                Thread.Sleep(3000);
            }
        }

        static private void Generate(string horse)
        {
            Send(horse);
        }


        static private void Send(string msg)
        {
            clientSocket.Send(Encoding.UTF8.GetBytes(msg));
            Console.WriteLine("Msg " + i + ": " + msg);
            i++;
        }


    }
}
