using System;

namespace Minter.Networking.Packets.Registry
{
    public interface IPacketRegistry
    {
        /// <summary>
        /// Returns packet by id and state
        /// </summary>
        /// <param name="packetId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T? ResolvePacket<T>(int packetId) where T : IPacket;

        /// <summary>
        /// Registers new connection state with packets
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="configure"></param>
        void RegisterPacket<T>(int packetId, Action<IPacketConfigure<T>> configure) where T : IPacket;
    }
}
