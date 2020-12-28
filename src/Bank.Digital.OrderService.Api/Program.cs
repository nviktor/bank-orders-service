using FluentMigrator.Runner;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Bank.Digital.OrderService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IMigrationRunner>()
                    .MigrateUp();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, false);
                })
                .UseStartup<Startup>()
                .UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));
    }
}