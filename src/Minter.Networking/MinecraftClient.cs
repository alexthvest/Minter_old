using System;
using System.IO;
using System.Threading;
using Minter.Networking.Packets;
using Minter.Networking.Packets.Handshaking;
using Minter.Networking.Packets.Registry;
using Minter.Networking.Protocol;

namespace Minter.Networking
{
    public class MinecraftClient : IDisposable
    {
        public ConnectionState ConnectionState { get; set; }
        
        private readonly Stream _stream;
        
        private readonly IConnectionStateRegistry _connectionStateRegistry;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="connectionStateRegistry"></param>
        public MinecraftClient(Stream stream, IConnectionStateRegistry connectionStateRegistry)
        {
            _stream = stream;

            ConnectionState = ConnectionState.Handshaking;
            _connectionStateRegistry = connectionStateRegistry;

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

                    var packetRegistry = _connectionStateRegistry.ResolvePackets(ConnectionState);

                    if (packetRegistry is null)
                    {
                        Console.WriteLine($"Unregistered connection state: {ConnectionState}");
                        continue;
                    }
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
