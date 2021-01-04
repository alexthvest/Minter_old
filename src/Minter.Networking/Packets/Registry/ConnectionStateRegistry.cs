using System;
using System.Collections.Generic;

namespace Minter.Networking.Packets.Registry
{
    public class ConnectionStateRegistry : IConnectionStateRegistry
    {
        private readonly IDictionary<ConnectionState, IPacketRegistry> _states;

        /// <summary>
        /// 
        /// </summary>
        public ConnectionStateRegistry() :
            this(new Dictionary<ConnectionState, IPacketRegistry>()) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="states"></param>
        public ConnectionStateRegistry(IDictionary<ConnectionState, IPacketRegistry> states)
        {
            _states = states;
        }

        public IPacketRegistry? ResolvePackets(ConnectionState connectionState)
        {
            if (_states.TryGetValue(connectionState, out var packetRegistry))
            {
                return packetRegistry;
            }

            return null;
        }

        public void RegisterConnectionState(ConnectionState connectionState, Action<IPacketRegistry> configure)
        {
            // var packetRegistry = new PacketRegistry();
            //
            // configure.Invoke(packetRegistry);
            //
            // _states[connectionState] = packetRegistry;
        }
    }
}
