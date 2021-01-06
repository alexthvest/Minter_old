using System.Runtime.InteropServices;

namespace Minter.Networking.Packets.Registry
{
    public interface IPacketRegistry
    {
        /// <summary>
        /// Gets packet by id and connection state
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <param name="packet"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool TryResolvePacket<T>(int packetId, ConnectionState state, out T packet) where T : IPacket;

        /// <summary>
        /// Gets packet by and connection state
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        bool TryResolvePacket(int packetId, ConnectionState state, out IPacket packet);

        /// <summary>
        /// Registers new packet
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="state"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterPacket<T>(int packetId, ConnectionState state) where T : IPacket;
    }
}
