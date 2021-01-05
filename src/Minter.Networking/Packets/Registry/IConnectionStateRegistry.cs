using System;

namespace Minter.Networking.Packets.Registry
{
    public interface IConnectionStateRegistry
    {
        /// <summary>
        /// Returns packet registry by connection state
        /// </summary>
        /// <param name="connectionState"></param>
        /// <returns></returns>
        IPacketRegistry? ResolvePackets(ConnectionState connectionState);

        /// <summary>
        /// Registers new connection state
        /// </summary>
        /// <param name="connectionState"></param>
        /// <param name="packets"></param>
        void RegisterConnectionState(ConnectionState connectionState, Action<IPacketRegistry> packets);
    }
}
