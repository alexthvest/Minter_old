using Minter.Networking.Packets.Handlers;
using Minter.Networking.Protocol;

namespace Minter.Networking.Packets.Handshaking
{
    public class HandshakingPacketReader : IPacketReader<HandshakingPacket>
    {
        public HandshakingPacket ReadPacket(MinecraftProtocolReader reader)
        {
            var protocol = reader.ReadVarInt();
            var hostname = reader.ReadString();
            var port = reader.ReadUInt16();
            var nextState = (ConnectionState) reader.ReadVarInt();
            
            return new HandshakingPacket(protocol, hostname, port, nextState);
        }
    }
}
