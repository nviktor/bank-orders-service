namespace Bank.Digital.OrderService.Integration.RabbitMq
{
    /// <summary>
    /// Параметры подключения к RabbitMq
    /// </summary>
    public class MqCredentials
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}
