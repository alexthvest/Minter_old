using System;
using System.Collections.Generic;

namespace Minter.Networking.Packets.Registry
{
    public class ConnectionStateRegistry : IConnectionStateRegistry
    {
        private readonly IDictionary<ConnectionState, IPacketRegistry> _packetRegistries;
        
        /// <summary>
        /// 
        /// </summary>
        public ConnectionStateRegistry() :
            this(new Dictionary<ConnectionState, IPacketRegistry>()) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetRegistries"></param>
        public ConnectionStateRegistry(IDictionary<ConnectionState, IPacketRegistry> packetRegistries)
        {
            _packetRegistries = packetRegistries;
        }

        public IPacketRegistry? ResolvePacketRegistry(ConnectionState state)
        {
            if (_packetRegistries.TryGetValue(state, out var packetRegistry))
            {
                return packetRegistry;
            }

            return null;
        }

        public void RegisterConnectionState(ConnectionState state, Action<IPacketRegistry> packetRegistry)
        {
            var registry = new PacketRegistry();
            
            packetRegistry.Invoke(registry);

            _packetRegistries[state] = registry;
        }
    }
}
