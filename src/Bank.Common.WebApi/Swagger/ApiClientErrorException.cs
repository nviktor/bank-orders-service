using System;
using System.Collections.Generic;

namespace Bank.Common.WebApi.Swagger
{
    /// <summary>
    /// Исключение генерируемое АПИ клиентами, сгенерированными nswag
    /// </summary>
    public class ApiClientErrorException : Exception
    {
        private static readonly int _responseTruncateLength = 512;

        /// <inheritdoc />
        public ApiClientErrorException(
            string message,
            int statusCode,
            string response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers,
            Exception innerException)
            : base(
                $"{message}\nStatus: {statusCode}{FormatResponse(response)}",
                innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        /// <summary>
        /// Http код ответа АПИ
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Ответ АПИ
        /// </summary>
        public string Response { get; }

        /// <summary>
        /// Коллекция заголовков ответа
        /// </summary>
        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

        private static string FormatResponse(string response)
        {
            var r = response?.Substring(
                0,
                response.Length >= _responseTruncateLength ? _responseTruncateLength : response.Length);

            return string.IsNullOrWhiteSpace(r) ? "" : "\nResponse:\n" + r;
        }
    }
}
