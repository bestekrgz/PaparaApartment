using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using PaparaApartment.Core.Utilities.IoC;

namespace PaparaApartment.Core.DependencyInjection
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services) 
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
