using System.Net;
using Bank.Digital.OrderService.DAL.Abstractions.Exceptions;
using Bank.Digital.OrderService.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Bank.Digital.OrderService.Api.Infrastructure
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var exception = context.Exception;
            var response = context.HttpContext.Response;
            response.ContentType = "text/plain";

            switch (exception)
            {
                case EntityNotFoundException _:

                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.WriteAsync(exception.Message).Wait();

                    break;

                case BusinessValidationException _:

                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    response.WriteAsync(exception.Message).Wait();

                    break;

                default:

                    _logger.LogError(default(EventId), exception, exception.Message);

                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.WriteAsync(exception.Message).Wait();
                    break;
            }
        }
    }
}
