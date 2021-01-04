namespace Minter.Networking.Packets.Handlers
{
    public interface IPacketHandler
    {
        
    }
    
    public interface IPacketHandler<T> : IPacketHandler
        where T : IPacket
    {
        /// <summary>
        /// Handles packet
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        void HandlePacket(MinecraftClient client, T packet);
    }
}
