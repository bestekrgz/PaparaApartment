using Microsoft.Extensions.DependencyInjection;


namespace PaparaApartment.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection collection);
    }
}
