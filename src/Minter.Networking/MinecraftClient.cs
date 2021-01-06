using System;
using System.IO;
using System.Threading;
using Minter.Networking.Packets;
using Minter.Networking.Registry;
using Minter.Networking.Protocol;

namespace Minter.Networking
{
    public class MinecraftClient : IDisposable
    {
        public ConnectionState ConnectionState { get; set; }

        private readonly Stream _stream;

        private readonly IPacketRegistry _packetRegistry;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="packetRegistry"></param>
        public MinecraftClient(Stream stream, IPacketRegistry packetRegistry)
        {
            _stream = stream;

            ConnectionState = ConnectionState.Handshaking;
            _packetRegistry = packetRegistry;

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        /// <summary>
        /// Starts new connection
        /// </summary>
        public void Start()
        {
            try
            {
                using var reader = new MinecraftProtocolReader(_stream);

                while (!_cancellationToken.IsCancellationRequested)
                {
                    var packetLength = reader.ReadVarInt();
                    var packetId = reader.ReadVarInt();

                    if (!_packetRegistry.TryResolvePacketReader(packetId, ConnectionState, out var packetReader))
                    {
                        Console.WriteLine($"No reader was found for packet: {ConnectionState}:{packetId}");
                        continue;
                    }

                    if (!_packetRegistry.TryResolvePacketHandler(packetId, ConnectionState, out var packetHandler))
                    {
                        Console.WriteLine($"No handler was found for packet: {ConnectionState}:{packetId}");
                        continue;
                    }

                    var packet = packetReader!.ReadPacket(reader);
                    packetHandler!.HandlePacket(this, packet);
                }
            }
            catch (EndOfStreamException) {}
            catch (Exception e)
            {
                // TODO: Error logging
                Console.WriteLine(e);
            }
            finally
            {
                Disconnect();
            }
        }

        /// <summary>
        /// Disconnects client
        /// </summary>
        public void Disconnect()
        {
            _cancellationTokenSource.Cancel();
            _stream.Close();
        }

        public void Dispose()
        {
            _stream.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }
}
