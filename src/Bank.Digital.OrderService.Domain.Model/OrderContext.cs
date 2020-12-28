using System;

namespace Bank.Digital.OrderService.Domain.Model
{
    public class OrderContext
    {
        /// <summary>
        /// IP адрес с которого была создана заявка
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Id заявки
        /// </summary>
        public Guid OrderId { get; set; }

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
