using System;
using Microsoft.Extensions.Hosting;

namespace Minter
{
    public static class MinterHost
    {
        /// <summary>
        /// Initializes a new instance of the HostBuilder class with Minter configuration.
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IHostBuilder CreateBuilder(Action<IMinterBuilder>? configure = null)
        {
            return Host.CreateDefaultBuilder().ConfigureMinterDefaults(configure);
        }

        /// <summary>
        /// Initializes a new instance of the Host class with Minter configuration and runs it.
        /// </summary>
        /// <param name="configure"></param>
        public static void ConfigureAndRun(Action<IMinterBuilder>? configure = null)
        {
            CreateBuilder(configure).Build().Run();
        }
    }
}
