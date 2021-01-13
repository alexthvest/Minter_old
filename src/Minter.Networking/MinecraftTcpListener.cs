using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Minter.Networking
{
    public class MinecraftTcpListener
    {
        private readonly TcpListener _listener;
        private bool _listening;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public MinecraftTcpListener(IPAddress address, int port)
            : this(new IPEndPoint(address, port)) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        public MinecraftTcpListener(IPEndPoint endpoint)
        {
            _listener = new TcpListener(endpoint);
            _listening = false;
        }

        /// <summary>
        /// Starts tcp listener
        /// </summary>
        /// <returns></returns>
        public async Task ListenAsync()
        {
            _listening = true;
            _listener.Start();

            while (_listening)
            {
                var client = await _listener.AcceptTcpClientAsync();
                new Thread(() => HandleConnection(client)).Start();
            }
        }

        /// <summary>
        /// Stops tcp listener
        /// </summary>
        public void Stop()
        {
            _listening = false;
            _listener.Stop();
        }

        private void HandleConnection(TcpClient tcpClient)
        {
            using var stream = tcpClient.GetStream();
            using var client = new MinecraftClient(stream);

            try
            {
                client.Handle();
            }
            catch (EndOfStreamException) {}
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client.Disconnect();
            }
        }
    }
}
