using System.Net;
using Minter.Core;

namespace Minter.Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var server = new MinecraftServer(IPAddress.Any, 25565);

            server.Listen();
        }
    }
}
