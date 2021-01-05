namespace Minter.Networking.Packets.Handshaking
{
    public record HandshakingPacket(int ProtocolVersion, string Hostname, int Port, ConnectionState NextState) : IPacket;
}
