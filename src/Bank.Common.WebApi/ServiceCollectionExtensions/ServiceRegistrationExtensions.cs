using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System.Linq;

namespace Bank.Common.WebApi.ServiceCollectionExtensions
{
    /// <summary>
    /// Расширения регистрации общих служб DI
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Добавление AutoMapper. Регистрация профилей из всех зависимостей проекта
        /// </summary>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var dependencyContext = DependencyContext.Default;

            var assembliesToScan = dependencyContext.RuntimeLibraries
                .SelectMany(l => l.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load));

            var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var profiles =
                allTypes
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);

            var config = new MapperConfiguration(cfg => {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            services.AddScoped<IMapper>(sp =>
                new Mapper(config, sp.GetService));

            return services;
        }
    }
}
