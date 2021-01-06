using System;
using System.Collections.Generic;
using Minter.Networking.Packets;
using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Registry
{
    public class PacketBuilder<T> : IPacketBuilder<T> where T : IPacket
    {
        private readonly PacketInfo _packetInfo;
        private readonly IDictionary<PacketInfo, IPacketReader> _readers;
        private readonly IDictionary<PacketInfo, IPacketHandler> _handlers;

        public PacketBuilder(
            PacketInfo packetInfo,
            IDictionary<PacketInfo, IPacketReader> readers,
            IDictionary<PacketInfo, IPacketHandler> handlers
        )
        {
            _packetInfo = packetInfo;
            _readers = readers;
            _handlers = handlers;
        }

        public IPacketBuilder<T> UsePacketReader<TReader>() where TReader : IPacketReader<T>, new()
        {
            if (!_readers.ContainsKey(_packetInfo))
            {
                _readers[_packetInfo] = Activator.CreateInstance<TReader>();
            }

            return this;
        }

        public IPacketBuilder<T> UsePacketHandler<THandler>() where THandler : IPacketHandler<T>, new()
        {
            if (!_handlers.ContainsKey(_packetInfo))
            {
                _handlers[_packetInfo] = Activator.CreateInstance<THandler>();
            }
            
            return this;
        }
    }
}
