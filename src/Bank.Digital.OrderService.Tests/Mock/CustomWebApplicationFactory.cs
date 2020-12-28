using Bank.Digital.OrderService.DAL.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bank.Digital.OrderService.Tests.Mock
{
    /// <summary>
    /// https://docs.microsoft.com/ru-ru/aspnet/core/test/integration-tests?view=aspnetcore-2.2
    /// https://stackoverflow.com/questions/53507352/replace-service-with-mocked-using-webapplicationfactory
    /// https://github.com/aspnet/Mvc/issues/7976
    /// </summary>
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.UseEnvironment("Local");

            //Example to mock services

            //builder.ConfigureTestServices(services =>
            //{
            //    services.BuildServiceProvider();

            //    services.Replace(new ServiceDescriptor(
            //        typeof(IOrdersRepository), 
            //        typeof(MockOrdersRepository), 
            //        ServiceLifetime.Transient));
            //});
        }
    }
}
