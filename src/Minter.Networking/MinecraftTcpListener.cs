using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Minter.Networking.Readers;

namespace Minter.Networking
{
    public class MinecraftTcpListener
    {
        private readonly TcpListener _listener;
        private bool _listening;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public MinecraftTcpListener(IPAddress ipAddress, int port)
            : this(new IPEndPoint(ipAddress, port)) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipEndPoint"></param>
        public MinecraftTcpListener(IPEndPoint ipEndPoint)
        {
            _listener = new TcpListener(ipEndPoint);
            _listening = false;
        }

        /// <summary>
        /// Starts TCP listener
        /// </summary>
        public void Listen()
        {
            _listener.Start();
            _listening = true;

            while (_listening)
            {
                var client = _listener.AcceptTcpClient();
                new Thread(() => ConnectionCallback(client)).Start();
            }
        }

        /// <summary>
        /// Stops TCP listener
        /// </summary>
        public void StopListen()
        {
            _listening = false;
            _listener.Stop();
        }

        /// <summary>
        /// Handles tcp client
        /// </summary>
        /// <param name="client"></param>
        private void ConnectionCallback(TcpClient client)
        {
            try
            {
                var stream = client.GetStream();
                var reader = new MinecraftProtocolReader(stream);

                var packetLength = reader.ReadVarInt();
                var packetId = reader.ReadVarInt();

                Console.WriteLine($"Length: {packetLength}");
                Console.WriteLine($"PacketId: {packetId}");

                client.Close();
            }
            catch (Exception e)
            {
                // TODO: error logging

                Console.WriteLine(e);
                StopListen();
            }
        }
    }
}
