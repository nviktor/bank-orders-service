namespace Bank.Digital.OrderService.Integration.RabbitMq
{
    /// <summary>
    /// Конфигурация клиента RabbitMQ
    /// </summary>
    public class RabbitMqConfig
    {
        /// <summary>
        /// Путь к инстансу Rabbit
        /// </summary>
        public MqCredentials Credentials { get; set; }
    }
}
