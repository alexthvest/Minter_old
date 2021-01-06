using Minter.Networking.Packets;
using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Registry
{
    public interface IPacketRegistry
    {
        /// <summary>
        /// Gets packet reader by packet type
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        bool TryResolvePacketReader(int packetId, ConnectionState state, out IPacketReader? reader);
        
        /// <summary>
        /// Gets packet handler by packet type
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        bool TryResolvePacketHandler(int packetId, ConnectionState state, out IPacketHandler? handler);
        
        /// <summary>
        /// Registers new packet
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IPacketBuilder<T> RegisterPacket<T>(int packetId, ConnectionState state) where T : IPacket;
    }
}
