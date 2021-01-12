using Microsoft.Extensions.Hosting;
using Minter.Hosting;

namespace Minter.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CreateBuilder(args).Build().Run();
        }
        
        private static IHostBuilder CreateBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureMinterDefaults();
        }
    }
}
