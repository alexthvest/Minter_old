namespace Minter.Networking.Packets.Registry
{
    public interface IPacketRegistry
    {
        /// <summary>
        /// Gets packet by id
        /// </summary>
        /// <param name="packetId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T? ResolvePacket<T>(int packetId) where T : IPacket<T>, new();

        /// <summary>
        /// Gets packet by id
        /// </summary>
        /// <param name="packetId"></param>
        /// <returns></returns>
        IPacket? ResolvePacket(int packetId);
        
        /// <summary>
        /// Registers new packet
        /// </summary>
        /// <param name="packetId"></param>
        /// <typeparam name="T"></typeparam>
        void RegisterPacket<T>(int packetId) where T : IPacket<T>, new();
    }
}
