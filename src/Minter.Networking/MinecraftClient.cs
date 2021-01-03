using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Minter.Networking.Readers;

namespace Minter.Networking
{
    public class MinecraftClient : IDisposable
    {
        private readonly Stream _stream;

        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public MinecraftClient(Stream stream)
        {
            _stream = stream;

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

                    Console.WriteLine($"Length: {packetLength}");
                    Console.WriteLine($"PacketId: {packetId}");
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
