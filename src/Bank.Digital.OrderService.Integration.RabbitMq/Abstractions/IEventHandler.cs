using System.Threading.Tasks;

namespace Bank.Digital.OrderService.Integration.RabbitMq.Abstractions
{
    /// <summary>
    /// Обработчик события
    /// </summary>
    internal interface IEventHandler<in TMessage>
    {
        /// <summary>
        /// Handle new event
        /// </summary>
        Task HandleAsync(TMessage message);
    }
}