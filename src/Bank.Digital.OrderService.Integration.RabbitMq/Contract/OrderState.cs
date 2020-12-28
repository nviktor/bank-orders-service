namespace Bank.Digital.OrderService.Integration.RabbitMq.Contract
{
    internal enum OrderState
    {
        New,
        PushedToIbs,
        StopfactorsRejected,
        ClientHasDboRejected
    }
}