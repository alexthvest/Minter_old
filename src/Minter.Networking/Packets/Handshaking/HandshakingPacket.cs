namespace Minter.Networking.Packets.Handshaking
{
    public class HandshakingPacket : IPacket
    {
        public int Id => 0x00;

        public ConnectionState ConnectionState => ConnectionState.Handshaking;
        
        /// <summary>
        /// Contains protocol version (minecraft version)
        /// </summary>
        public int ProtocolVersion { get; }
        
        /// <summary>
        /// Contains server hostname
        /// </summary>
        public string Hostname { get; }
        
        /// <summary>
        /// Contains server port (default 25565)
        /// </summary>
        public int Port { get; }
        
        /// <summary>
        /// Contains next connection state
        /// </summary>
        public ConnectionState NextState { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="protocolVersion"></param>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <param name="nextState"></param>
        public HandshakingPacket(int protocolVersion, string hostname, int port, ConnectionState nextState)
        {
            ProtocolVersion = protocolVersion;
            Hostname = hostname;
            Port = port;
            NextState = nextState;
        }
    }
}
