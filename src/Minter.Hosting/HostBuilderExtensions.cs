using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Minter.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureMinterDefaults(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddHostedService<ServerHostedService>();
            });

            return hostBuilder;
        }
    }
}
