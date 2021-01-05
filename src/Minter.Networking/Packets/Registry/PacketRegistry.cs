using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Minter.Networking.Packets.Registry
{
    public class PacketRegistry : IPacketRegistry
    {
        private readonly IDictionary<int, IPacket> _packets;

        /// <summary>
        /// 
        /// </summary>
        public PacketRegistry() :
            this(new Dictionary<int, IPacket>()) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packets"></param>
        public PacketRegistry(IDictionary<int, IPacket> packets)
        {
            _packets = packets;
        }

        public T? ResolvePacket<T>(int packetId) where T : IPacket<T>, new()
        {
            if (_packets.TryGetValue(packetId, out var p) && p is T packet)
            {
                return packet;
            }

            return null;
        }

        public IPacket? ResolvePacket(int packetId)
        {
            if (_packets.TryGetValue(packetId, out var packet))
            {
                return packet;
            }

            return null;
        }

        public void RegisterPacket<T>(int packetId) where T : IPacket<T>, new()
        {
            _packets[packetId] = Activator.CreateInstance<T>();
        }
    }
}
