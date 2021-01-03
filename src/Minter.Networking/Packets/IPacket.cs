namespace Minter.Networking.Packets
{
    public interface IPacket
    {
        /// <summary>
        /// Packet id
        /// </summary>
        int Id { get; }
        
        /// <summary>
        /// Packet connection state
        /// </summary>
        ConnectionState ConnectionState { get; }
    }
}
