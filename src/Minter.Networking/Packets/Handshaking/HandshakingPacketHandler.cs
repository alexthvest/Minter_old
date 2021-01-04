using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Packets.Handshaking
{
    public class HandshakingPacketHandler : IPacketHandler<HandshakingPacket>
    {
        public void HandlePacket(MinecraftClient client, HandshakingPacket packet)
        {
            client.ConnectionState = packet.NextState;
        }
    }
}
