using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Digital.OrderService.Api.Contract.Orders
{
    public class OrderCreateRequest
    {
        /// <summary>
        /// Аппликант/проспект/ человек желающий оформить услугу
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int? ApplicantId { get; set; }

        /// <summary>
        /// Дата создания заявки
        /// </summary>
        [Required]
        public DateTimeOffset? DateAdd { get; set; }

        /// <summary>
        /// ID открываемого продукта ( РКО, гарантии, кредит)
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int? ProductId { get; set; }

        /// <summary>
        /// IP адрес с которого была создана заявка
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// UTM метка
        /// </summary>
        public string Utm { get; set; }

        /// <summary>
        /// аналитика от гугла
        /// </summary>
        public string GoogleAnalyticsId { get; set; }

        /// <summary>
        /// город
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// браузер клиента
        /// </summary>
        public string Browser { get; set; }

        /// <summary>
        /// юзер агент браузера
        /// </summary>
        public string BrowserUserAgent { get; set; }
    }
}