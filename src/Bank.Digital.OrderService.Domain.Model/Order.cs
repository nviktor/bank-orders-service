using System;

namespace Bank.Digital.OrderService.Domain.Model
{
    /// <summary>
    /// Заявка на продукт
    /// </summary>
    public class Order
    {
        /// <summary>
        /// ID заявки
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Апликант/проспект/ человек желающий оформить услугу
        /// </summary>
        public int ApplicantId { get; set; }

        /// <summary>
        /// Состояние заявки
        /// </summary>
        public OrderState State { get; set; }

        /// <summary>
        /// Дата создания заявки
        /// </summary>
        public DateTimeOffset DateAdd { get; set; }

        /// <summary>
        /// ID открываемого продукта ( РКО, гарантии, кредит)
        /// </summary>
        public int ProductId { get; set; }
    }
}