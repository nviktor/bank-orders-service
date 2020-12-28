using System.Threading;
using System.Threading.Tasks;

namespace Bank.Common.WebApi.HostedServices
{
    /// <summary>
    /// One time job, executes on service start. Executes in separate scope so use DI
    /// </summary>
    /// <remarks>
    /// DI registration:
    /// services.AddTransient{IStartupTask, IStartupTaskImplementation}();
    /// services.AddHostedService{StartupperHostedService}()
    /// </remarks>
    public interface IStartupTask
    {
        /// <summary>
        /// Execute
        /// </summary>
        Task Execute(CancellationToken cancellationToken);
    }
}
