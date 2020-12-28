using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bank.Digital.OrderService.Api.Infrastructure
{
    public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            
            options.DescribeAllEnumsAsStrings();
            
            // Set the comments path for the Swagger JSON and UI.
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

            //The same for contract
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Bank.Digital.OrderService.Api.Contract.xml"));
        }

        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info
            {
                Title = "OrderService API",
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}