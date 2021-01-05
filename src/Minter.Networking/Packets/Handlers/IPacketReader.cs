using Minter.Networking.Protocol;

namespace Minter.Networking.Packets.Handlers
{
    public interface IPacketReader
    {
        /// <summary>
        /// Reads packet and returns it
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        IPacket ReadPacket(MinecraftProtocolReader reader);
    }

    public interface IPacketReader<T> : IPacketReader
        where T : IPacket<T>
    {
        /// <summary>
        /// Reads packet and returns it
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        new T ReadPacket(MinecraftProtocolReader reader);

        IPacket IPacketReader.ReadPacket(MinecraftProtocolReader reader) =>
            ReadPacket(reader);
    }
}
