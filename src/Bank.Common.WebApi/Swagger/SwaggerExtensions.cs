using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Bank.Common.WebApi.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, string apiName)
        {
            return services.AddSwaggerGen(c => { c.SwaggerDoc(apiName, new Info { Title = apiName }); });
        }
    }

}