using System.Net;
using System.Net.Sockets;
using System.Threading;
using Minter.Networking.Packets;
using Minter.Networking.Packets.Handshaking;
using Minter.Networking.Packets.Registry;

namespace Minter.Networking
{
    public class MinecraftTcpListener
    {
        private readonly TcpListener _listener;
        private bool _listening;

        private readonly IConnectionStateRegistry _connectionStateRegistry;

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

            _connectionStateRegistry = new ConnectionStateRegistry();
            RegisterPackets();
        }

        /// <summary>
        /// Registers connection states with packets
        /// </summary>
        private void RegisterPackets()
        {
            _connectionStateRegistry.RegisterConnectionState(ConnectionState.Handshaking, packetRegistry =>
            {
                packetRegistry.RegisterPacket<HandshakingPacket>(0x00, packet =>
                {
                    packet.UsePacketReader<HandshakingPacketReader>();
                    
                });
            });
            
            _connectionStateRegistry.RegisterConnectionState(ConnectionState.Status, packetRegistry =>
            {
                packetRegistry.RegisterPacket<HandshakingPacket>(0x00, packet =>
                {
                    
                });
            });
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
        /// <param name="tcpClient"></param>
        private void ConnectionCallback(TcpClient tcpClient)
        {
            using var stream = tcpClient.GetStream();
            using var client = new MinecraftClient(stream, _connectionStateRegistry);
            
            client.Start();
        }
    }
}
