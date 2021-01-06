using System.Collections.Generic;
using Minter.Networking.Packets;
using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Registry
{
    public class PacketRegistry : IPacketRegistry
    {
        private readonly Dictionary<PacketInfo, IPacketReader> _readers = new();
        private readonly Dictionary<PacketInfo, IPacketHandler> _handlers = new();

        public bool TryResolvePacketReader(int packetId, ConnectionState state, out IPacketReader? reader)
        {
            var packetInfo = new PacketInfo(packetId, state);

            if (_readers.TryGetValue(packetInfo, out var result))
            {
                reader = result;
                return true;
            }

            reader = null;
            return false;
        }

        public bool TryResolvePacketHandler(int packetId, ConnectionState state, out IPacketHandler? handler)
        {
            var packetInfo = new PacketInfo(packetId, state);

            if (_handlers.TryGetValue(packetInfo, out var result))
            {
                handler = result;
                return true;
            }

            handler = null;
            return false;
        }

        public IPacketBuilder<T> RegisterPacket<T>(int packetId, ConnectionState state) where T : IPacket
        {
            var packetInfo = new PacketInfo(packetId, state);
            return new PacketBuilder<T>(packetInfo, _readers, _handlers);
        }
    }
}
