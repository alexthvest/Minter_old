using Minter.Networking.Packets.Handlers;

namespace Minter.Networking.Packets.Registry
{
    public interface IPacketConfigurationBuilder<T> where T : IPacket
    {
        /// <summary>
        /// Sets reader for packet
        /// </summary>
        /// <typeparam name="TReader"></typeparam>
        void UsePacketReader<TReader>() where TReader : IPacketReader<T>, new();

        /// <summary>
        /// Sets handler for packet
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        void UsePacketHandler<THandler>() where THandler : IPacketHandler<T>, new();
    }
}
