using Minter.Networking.Packets;
using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Registry
{
    public interface IPacketBuilder<T> where T : IPacket
    {
        /// <summary>
        /// Sets reader for packet
        /// </summary>
        /// <typeparam name="TReader"></typeparam>
        /// <returns></returns>
        IPacketBuilder<T> UsePacketReader<TReader>() where TReader : IPacketReader<T>, new();
        
        /// <summary>
        /// Sets handler for packet
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <returns></returns>
        IPacketBuilder<T> UsePacketHandler<THandler>() where THandler : IPacketHandler<T>, new();
    }
}
