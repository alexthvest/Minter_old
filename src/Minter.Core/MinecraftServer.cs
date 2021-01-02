using System;
using System.Net;
using Minter.Networking;

namespace Minter.Core
{
    public class MinecraftServer
    {
        private readonly IPEndPoint _ipEndPoint;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public MinecraftServer(IPAddress ipAddress, int port)
            : this(new IPEndPoint(ipAddress, port)) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ipEndPoint"></param>
        public MinecraftServer(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
        }

        /// <summary>
        /// Starts server listening
        /// </summary>
        public void Listen()
        {
            var listener = new MinecraftTcpListener(_ipEndPoint);
            listener.Listen();
        }
    }
}
