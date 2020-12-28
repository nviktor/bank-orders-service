using System;

namespace Bank.Digital.OrderService.Domain.Exceptions
{
    /// <summary>
    /// Ошибки бизнес-валидации данных
    /// </summary>
    public class BusinessValidationException : Exception
    {
        /// <inheritdoc />
        public BusinessValidationException(string message) : base(message)
        {
        }
    }
}
