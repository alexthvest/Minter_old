using Minter.Networking.Protocol;

namespace Minter.Networking.Packets.Handlers
{
    public interface IPacketReader
    {
        
    }
    
    public interface IPacketReader<T> : IPacketReader
        where T : IPacket
    {
        /// <summary>
        /// Reads packet and returns it
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        T ReadPacket(MinecraftProtocolReader reader);
    }
}
