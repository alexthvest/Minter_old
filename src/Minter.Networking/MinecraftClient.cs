using System;
using System.IO;
using System.Threading;
using Minter.Networking.Protocol;

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
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        /// <summary>
        /// Handles client packets
        /// </summary>
        public void Handle()
        {
            using var reader = new MinecraftProtocolReader(_stream);

            while (!_cancellationToken.IsCancellationRequested)
            {
                var packetLength = reader.ReadVarInt();
                var packetId = reader.ReadVarInt();

                Console.WriteLine($"Packet: 0x{packetId:X2}");
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
            _cancellationTokenSource.Dispose();
            _stream.Dispose();
        }
    }
}
