using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Packets
{
    public interface IPacket
    {
        int Id { get; }
        
        ConnectionState ConnectionState { get; }
        
        IPacketReader Reader { get; }

        IPacketHandler Handler { get; }
    }

    public interface IPacket<T> : IPacket
        where T : IPacket<T>
    {
        new IPacketReader<T> Reader { get; }

        new IPacketHandler<T> Handler { get; }

        IPacketReader IPacket.Reader => Reader;

        IPacketHandler IPacket.Handler => Handler;
    }
}
