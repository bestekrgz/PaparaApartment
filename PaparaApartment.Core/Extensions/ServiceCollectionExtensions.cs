using Microsoft.Extensions.DependencyInjection;
using PaparaApartment.Core.Utilities.IoC;

namespace PaparaApartment.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(services);
            }
            return ServiceTool.Create(services);
        }
    }
}

