using System;
using System.IO;
using System.Threading;
using Minter.Networking.Packets;
using Minter.Networking.Packets.Handshaking;
using Minter.Networking.Readers;

namespace Minter.Networking
{
    public class MinecraftClient : IDisposable
    {
        private readonly Stream _stream;
        private ConnectionState _connectionState;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public MinecraftClient(Stream stream)
        {
            _stream = stream;
            _connectionState = ConnectionState.Handshaking;

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

                    if (packetId == 0x00 && _connectionState == ConnectionState.Handshaking)
                    {
                        Console.WriteLine(">> Handshaking");

                        var packet = new HandshakingPacket(
                            reader.ReadVarInt(), // protocol version
                            reader.ReadString(), // hostname
                            reader.ReadUInt16(), // port
                            (ConnectionState) reader.ReadVarInt() // next connection state
                        );

                        _connectionState = packet.NextState;
                    }
                    else
                    {
                        Console.WriteLine($"Unknown packet: {packetId}");
                    }
                }
            }
            catch (EndOfStreamException) {}
            catch (IOException) {}
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
