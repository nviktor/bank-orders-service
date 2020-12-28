using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bank.Common.WebApi.HostedServices
{
    /// <summary>
    /// Разовое выполнение кода на старте сервиса.
    /// </summary>
    /// <remarks>
    /// DI registration:
    /// services.AddTransient{IStartupTask, IStartupTaskImplementation}();
    /// services.AddHostedService{StartupperHostedService}()
    /// </remarks>
    public class StartupperHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public StartupperHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var tasks = scope.ServiceProvider.GetServices<IStartupTask>();

                foreach (var task in tasks)
                {
                    await task.Execute(cancellationToken);
                }
            }
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
