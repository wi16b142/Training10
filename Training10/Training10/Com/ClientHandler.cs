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
    public class ClientHandler
    {
        private Socket clientSocket;
        private Action<string> guiUpdate;
        const int port = 10100;
        Thread receivingThread;
        byte[] buffer = new byte[512];

        public ClientHandler(Socket socket, Action<string> guiUpdate)
        {
            this.clientSocket = socket;
            this.guiUpdate = guiUpdate;

            StartReceiving();
        }

        private void StartReceiving()
        {
            receivingThread = new Thread(Receive);
            receivingThread.IsBackground = true;
            receivingThread.Start();
        }
        private void Receive()
        {
            while (receivingThread.IsAlive)
            {
                int length = clientSocket.Receive(buffer);
                guiUpdate(Encoding.UTF8.GetString(buffer, 0, length));
            }
        }

    }
}
