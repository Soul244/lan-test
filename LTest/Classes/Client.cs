using System;
using System.Net;
using System.Net.Sockets;

namespace LTest.Classes
{
    public class Client
    {
        private readonly string _id;
        private readonly IPEndPoint _endPoint;

        public Socket Socket { get; }

        public string GetId()
        {
            return _id;
        }

        public IPEndPoint GetEndPoint()
        {
            return _endPoint;
        }


        public Client(Socket accepted)
        {
            Socket = accepted;
            _id = Guid.NewGuid().ToString();
            _endPoint = (IPEndPoint)Socket.RemoteEndPoint;
            Socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, Callback, null);
        }

        private void Callback(IAsyncResult ar)
        {
                Socket.EndReceive(ar);
                var buf = new byte[8192];
                var recData = Socket.Receive(buf, buf.Length, 0);
                if (recData<buf.Length)
                {
                    Array.Resize(ref buf,recData);
                }
                Received?.Invoke(this, buf);
            Socket.BeginReceive(new byte[] { 0 }, 0, 0, 0, Callback, null);
        }



        public delegate void ClientReceivedHandler(Client sender, byte[] data);
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;

        protected virtual void OnDisconnected()
        {
            Socket.Close();
            Disconnected?.Invoke(this);
        }
    }
}
