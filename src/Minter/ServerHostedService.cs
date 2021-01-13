using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Minter
{
    public class ServerHostedService : IHostedService
    {
        private readonly ILogger<ServerHostedService> _logger;

        public ServerHostedService(ILogger<ServerHostedService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void StartServer()
        {
            _logger.LogInformation("Server is started");
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(StartServer, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
