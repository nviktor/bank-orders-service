using System;
using System.Threading.Tasks;
using Bank.Digital.OrderService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Digital.OrderService.Api.Controllers
{
    /// <summary>
    /// Тестовые АПИ
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestsController : ControllerBase
    {
        [HttpGet("hadoop")]
        public async Task<IActionResult> Get(string filename)
        {
            var path = $"{Environment.CurrentDirectory}/uploads/{filename}";

            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            return File(System.IO.File.OpenRead(path), "application/octet-stream", filename);
        }

        [HttpPost("exception")]
        public IActionResult Exception(string name)
        {
            switch (name)
            {
                case nameof(BusinessValidationException):
                    throw new BusinessValidationException($"{nameof(BusinessValidationException)} debug error message");

                default:
                    throw new Exception($"{nameof(Exception)} debug error message");
            }
        }
    }
}
