namespace Bank.Digital.OrderService.Domain.Model
{
    public enum OrderState
    {
        /// <summary>
        /// Новая
        /// </summary>
        New = 1,

        /// <summary>
        /// В процессе заполнения
        /// </summary>
        Filling = 2,

        /// <summary>
        /// На проверке
        /// </summary>
        Revision = 3,

        /// <summary>
        /// Отклонена
        /// </summary>
        Rejected = 4
    }
}
