using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Minter
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
