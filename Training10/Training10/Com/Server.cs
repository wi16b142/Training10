using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Training10.Com
{
    public class Server
    {
        private Action<string> guiUpdate;
        const int port = 10100;
        Thread acceptingThread;
        Socket serverSocket;
        List<ClientHandler> Clients = new List<ClientHandler>();



        public Server(Action<string> guiUpdate)
        {
            this.guiUpdate = guiUpdate;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            serverSocket.Listen(10);

            StartAccepting();
            
        }

        private void StartAccepting()
        {
            acceptingThread = new Thread(Accept);
            acceptingThread.IsBackground = true;
            acceptingThread.Start();
        }

        private void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                Clients.Add(new ClientHandler(serverSocket.Accept(),guiUpdate));
            }
        }

        


    }
}
