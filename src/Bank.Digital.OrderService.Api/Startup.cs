using Bank.Common.WebApi.HostedServices;
using Bank.Common.WebApi.ServiceCollectionExtensions;
using Bank.Digital.OrderService.Api.Infrastructure;
using Bank.Digital.OrderService.DAL.Linq2Db;
using Bank.Digital.OrderService.Domain;
using Bank.Digital.OrderService.Integration.RabbitMq;
using Bank.Digital.OrderService.Migrations.OrderServiceDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.Digital.OrderService.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RabbitMqConfig>(_configuration.GetSection("Environment:RabbitMq"));

            services
                .AddLinq2DbDalServices()
                .AddDomainServices()
                .AddRabbitMqServices()
                .AddOrderServiceDbMigrations(_configuration)
                .AddHostedService<StartupperHostedService>();

            services.AddAutoMapper();

            services
                .AddApiVersioning()
                .AddVersionedApiExplorer(
                    options =>
                    {
                        options.GroupNameFormat = "VVVV";
                        options.SubstitutionFormat = "VVVV";
                        options.SubstituteApiVersionInUrl = true;
                    });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
            services.AddSwaggerGen();

            services.AddMvc(config =>
                {
                    config.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}