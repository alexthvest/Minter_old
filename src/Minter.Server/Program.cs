using System;
using System.Net;
using Minter.Core;

namespace Minter.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new MinecraftServer(IPAddress.Any, 25565);

            server.Listen();
        }
    }
}
