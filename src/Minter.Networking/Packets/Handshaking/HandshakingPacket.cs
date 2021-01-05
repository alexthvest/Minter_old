using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Packets.Handshaking
{
    public record HandshakingPacket(int ProtocolVersion, string Hostname, int Port, ConnectionState NextState) : IPacket<HandshakingPacket>
    {
        public int Id => 0x00;

        public ConnectionState ConnectionState => ConnectionState.Handshaking;

        public IPacketReader<HandshakingPacket> Reader => new HandshakingPacketReader();

        public IPacketHandler<HandshakingPacket> Handler => new HandshakingPacketHandler();
    }
}
