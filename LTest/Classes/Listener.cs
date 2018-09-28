using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;

namespace LTest.Classes
{
    public class Listener
    {
        private Socket _socket;
        private bool _listening;
        private List<Client> _clients;

        public bool Listening {get=>_listening; set=> _listening=value;}

        public int Port { get; }

        public List<Client> Clients { get => _clients; set => _clients = value; }

        public Listener(int port)
        {
            Port = port;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            if (_listening)
                return;
            _listening = true;
            _socket.Bind(new IPEndPoint(0, Port));
            _socket.Listen(0);
            _socket.BeginAccept(Callback, null);
        }

        public void Stop()
        {
            if (!_listening)
                return;
            _listening = false;
            _socket.Close();
            _socket.Dispose();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void Callback(IAsyncResult ar)
        {
            try
            {
                var socket = this._socket.EndAccept(ar);
                SocketAccepted?.Invoke(socket);
                _socket.BeginAccept(Callback, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void SendObject(object obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, obj);
            byte[] buffer = memoryStream.ToArray();
            foreach (var client in _clients)
            {
                client.Socket.Send(buffer);
            }
        }

        public void SendText(string text)
        {
            foreach (var client in _clients)
            {
                client.Socket.Send(Encoding.UTF8.GetBytes(text));
            }
        }

        public object GetObject(byte[] data)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream(data);
                if (ms != null)
                {
                    return formatter.Deserialize(ms);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public delegate void SocketAcceptedHandler(Socket e);
        public event SocketAcceptedHandler SocketAccepted;
    }
}
