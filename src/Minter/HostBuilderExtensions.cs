using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Minter
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureMinterDefaults(this IHostBuilder hostBuilder, Action<IMinterBuilder>? configure = null)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddHostedService<MinterHostedService>();
            });

            return hostBuilder;
        }
    }
}
