using System;

namespace Minter.Networking.Packets.Registry
{
    public interface IConnectionStateRegistry
    {
        /// <summary>
        /// Gets packet registry by connection state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        IPacketRegistry? ResolvePacketRegistry(ConnectionState state);
        
        /// <summary>
        /// Registers new connection state with packets
        /// </summary>
        /// <param name="state"></param>
        /// <param name="packetRegistry"></param>
        void RegisterConnectionState(ConnectionState state, Action<IPacketRegistry> packetRegistry);
    }
}
