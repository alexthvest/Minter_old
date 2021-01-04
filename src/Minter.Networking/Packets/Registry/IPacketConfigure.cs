using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Packets.Registry
{
    public interface IPacketConfigure<T> where T : IPacket
    {
        void UsePacketReader<U>() where U : IPacketReader, new();

        void UsePacketReader(IPacketReader reader);
    }
}
