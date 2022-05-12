using Owlet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Owlet.BaseClasses
{
    public class BaseNetworkWriter : INetworkWriter
    {
        private int port;
        private TcpClient client;
        private int[] openPorts;
        private NetworkStream stream;
        private Socket socket;

        public BaseNetworkWriter(string server, int port = 13370)
        {
            client = new TcpClient(server, this.port);
            stream = client.GetStream();
            socket = client.Client;
        }

        void readTCP()
        {
            byte[] buffer = new byte[1024];
            while (client.Connected)
            {
                if (socket.Poll(10000, SelectMode.SelectRead))
                {
                    socket.Receive(buffer);
                }
            }
        }

        //Host some networking stuff here
        public void Send<T>(T data)
        {
            if (!(data is string stringData))
                stringData = JsonSerializer.Serialize(data);

            var byteData = Encoding.UTF8.GetBytes(stringData);

            stream.Write(byteData, 0, byteData.Length);
        }

        public void Send<T>(IEnumerable<T> data)
        {
            var stringData = JsonSerializer.Serialize(data);

            var byteData = Encoding.UTF8.GetBytes(stringData);

        }
    }
}
