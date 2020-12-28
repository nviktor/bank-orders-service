namespace Bank.Digital.OrderService.Api.Contract.Orders
{
    public class OrderUpdateRequest
    {
        /// <summary>
        /// Id тарифа (совпадает с Id IBS)
        /// </summary>
        public int? TariffId { get; set; }

        /// <summary>
        /// Срок предоплаты в месяцах
        /// </summary>
        public int? PrepaymentMonth { get; set; }
    }
}
